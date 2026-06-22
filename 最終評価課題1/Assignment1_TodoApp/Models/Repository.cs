using Dapper;
using Microsoft.Data.SqlClient;

namespace Assignment1_TodoApp.Models;

public class Repository(IConfiguration config)
{
    // DB接続文字列
    private readonly string _connectionString = config.GetConnectionString("DefaultConnection") ?? "";

    // 一覧取得
    public List<TaskModel> GetTasks()
    {
        // クエリ文
        var sql = """
                SELECT *
                FROM TASKS
                ORDER BY TASK_ID ASC
                """;

        // DB接続
        using var connection = new SqlConnection(_connectionString);

        // データ取得
        var taskList = connection.Query<TaskModel>(sql);

        // List化して返却
        return [.. taskList];
    }

    // タスク単品
    public TaskModel? GetTargetTask(int id)
    {
        // クエリ文
        var sql = """
                SELECT *
                FROM TASKS
                WHERE TASK_ID = @id
                """;

        // DB接続
        using var connection = new SqlConnection(_connectionString);

        // データ取得
        var taskList = connection.QueryFirstOrDefault<TaskModel>(sql,new {id});

        return taskList;
    }

    // タスクの更新
    public int UpdateTask(UpdateDto newTask)
    {
        // クエリ文
        var sql = """
                UPDATE TASKS
                SET
                    TASK_NAME = @taskName,
                    ASSIGNEE = @assignee,
                    DUE_DATE = @dueDate,
                    STATUS = @status,
                    UPDATE_DATETIME = GETDATE()
                WHERE TASK_ID = @id
                """;

        // DB接続
        using var connection = new SqlConnection(_connectionString);

        // データ取得
        var affectRows = connection.Execute(
            sql,
            new
            {
                taskName = newTask.TASK_NAME,
                assignee = newTask.ASSIGNEE,
                dueDate = newTask.DUE_DATE,
                status = newTask.STATUS,
                id = newTask.TASK_ID
            });

        return affectRows;
    }
}
