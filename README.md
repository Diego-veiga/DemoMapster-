# 🧭 Resumo sobre Mapster

No desenvolvimento de software moderno, não é incomum nos depararmos com a necessidade de converter um objeto do tipo A para um tipo B — seja para criar uma *View*, seja para atender a um contrato de criação de objeto. Esse processo é conhecido como **mapeamento de objetos** (*object mapping*).

Para realizar esse tipo de tarefa, temos basicamente duas opções:

1. **Mapeamento manual**, onde fazemos a conversão “na unha”;
2. **Uso de bibliotecas de mapeamento**, que automatizam e facilitam esse processo.

Neste contexto, vamos focar em uma dessas bibliotecas: o **Mapster**.

---

## 🏗️ Estrutura do Projeto

Para exemplificar o uso do Mapster, vamos construir um pequeno projeto onde nossa entidade de domínio será a classe `Person`, com os seguintes atributos:

- `Id`
- `FirstName`
- `LastName`
- `Age`
- `CreatedAt`
- `UpdatedAt`

Como boa prática, evitamos expor diretamente os objetos de domínio. Por isso, criaremos duas classes adicionais:

- `CreatePerson` – usada como contrato de entrada, contendo: `FirstName`, `LastName`, `Age`;
- `PersonViewModel` – usada como contrato de saída (apresentação), contendo: `CompleteName` e `Age`.

---

## ⚙️ Configuração do Mapster

Para usar o Mapster, basta instalar a biblioteca:

```bash
dotnet add package Mapster.DependencyInjection

```
## 🛠️ Configuração Inicial

Após a instalação, registramos o Mapster como serviço usando um Extension Method. Para isso:

1. Criamos uma pasta chamada Mappers;

2. Dentro dela, criamos a classe MappingConfigurations, com um método estático responsável por registrar os mapeamentos;
```
public static class MappingConfiguration
{
    public static IServiceCollection RegisterMaps(this IServiceCollection services)
    {
        services.AddMapster();
        TypeAdapterConfig<Person, PersonViewModel>
            .NewConfig()
            .Map(p => p.CompletedName, d => $"{d.FirstName} {d.LastName}")
            .Map(p => p.Age, d => d.Age);
        return services;
    }
}
```

3. Chamamos esse método na classe Program, usando:

```
builder.Services.RegisterMaps();

```

## 🧪 Utilizando o Mapster
Com a configuração feita, já podemos usar o Mapster.

A primeira aplicação será o mapeamento de um objeto CreatePerson para um objeto do domínio Person.

⚠️ **Neste exemplo, não utilizaremos banco de dados — apenas objetos em memória.**

Para isso, criamos uma classe PersonRepository, com dois métodos principais:

Save(Person person) – salva uma pessoa na lista em memória;

GetAll() – retorna todas as pessoas.

## Exemplo de uso
```
// Criando novo objeto com base em CreatePerson
var domainPerson = createPerson.Adapt<Person>();

// Adaptando para um objeto já existente
existingPerson.Adapt(createPerson);
```

## 🎯 Mapeamento Personalizado

O segundo cenário é o mapeamento de um `Person` para um `PersonViewModel`, onde queremos unir `FirstName` e `LastName` na propriedade `CompleteName`.

Passos para configurar:
1. Volte à classe MappingConfigurations;

2. Adicione a configuração personalizada com TypeAdapterConfig.
```
TypeAdapterConfig<Person, PersonViewModel>.NewConfig()
    .Map(dest => dest.CompleteName, 
         src => $"{src.FirstName} {src.LastName}")
    .Map(dest => dest.Age, src => src.Age);
```

### Na controller ou serviço:

Utilize ProjectToType<T>() para transformar o IQueryable<Person> em IQueryable<PersonViewModel>:
```
var result = _repository
    .GetAll() // retorna IQueryable<Person>
    .ProjectToType<PersonViewModel>()
    .ToList();

```
🧠 Isso é possível porque ProjectToType estende IQueryable e permite projeções otimizadas com LINQ.

## ✅ Conclusão
O Mapster é uma biblioteca leve, rápida e poderosa para o mapeamento de objetos em .NET. Ele oferece uma excelente alternativa ao AutoMapper, com:

Suporte a Dependency Injection;

Mapeamentos manuais e personalizados;

Suporte a LINQ com projeções via ProjectToType;

Performance superior em cenários mais simples e comuns.