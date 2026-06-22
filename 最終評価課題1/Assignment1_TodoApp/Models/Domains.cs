namespace Assignment1_TodoApp.Models;

public record TaskModel(
    int TASK_ID,
    string TASK_NAME,
    string ASSIGNEE,
    DateTime DUE_DATE,
    string STATUS,
    DateTime CREATE_DATETIME,
    DateTime UPDATE_DATETIME
);

public record UpdateDto(
    int TASK_ID,
    string TASK_NAME,
    string ASSIGNEE,
    DateTime DUE_DATE,
    string STATUS
);
