# Security Questions Coding Exercise

## Data

Data is stored in JSON text files.

- **data.json** - user data, including stored questions and answers
- **questions.json** - available questions

Upon initial build, **data.json** is populated with seed user data. 

When the user provides a name, JSON data is read from disk and deserialized into model objects as described below.

As questions and answers are updated in the Store Flow, the updated user data is serialized and persisted back to the file system.

### Initial contents of **data.json**

```
{
  "user": [
    {
      "name": "Lee",
      "question": [
        {
          "text": "In what city were you born?",
          "answer": "Fort Kent"
        },
        {
          "text": "What is the name of your favorite pet?",
          "answer": "Eddie"
        },
        {
          "text": "What was the make of your first car?",
          "answer": "Malibu"
        }
      ]
    },
    {
      "name": "Tami",
      "question": [
        {
          "text": "In what city were you born?",
          "answer": "Decatur"
        },
        {
          "text": "What is your mother's maiden name?",
          "answer": "Perkins"
        },
        {
          "text": "What high school did you attend?",
          "answer": "Mt. Zion HS"
        }
      ]
    }
  ]
}
```

## Program architecture

The program is designed using a simple tiered SOA. Service layers are abstracted into interfaces to facilitate unit testing. Dependency injection is used to provide concrete implementions at runtime.

### Data

**IDataRepository** - contains methods to fetch and save data. By abstracting the data persistence using this interface, the main program remains agnostic to the actual data implementaton. This would allow a developer to swap out the JSON implementation for XML< plain text, SQLlite, or even an ORM.

## Services

**ISecurityQuestionService** -- provides an middleware API to act between the data respository and the program logic. Also provides convenience methods for such things as presenting stored questions to the user in random order, or checking provided answers against stored ones.

**IFlowService** -- contains the Initial, Answer, and Store flows as described in the requirements document.

## Models

**SecurityQuestions.Models.User** - represents a user.

**SecurityQuestions.Models.Question** - represents a question.


