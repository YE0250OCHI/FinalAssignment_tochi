using Assignment1_TodoApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Assignment1_TodoApp.Pages;

public class EditModel : PageModel
{
    // DB操作部
    private readonly Repository _repository;

    // 表示・編集用
    [BindProperty]
    public int TaskId { get; set; }

    // タスク名
    [StringLength(100, ErrorMessage = "タスク名は100文字以内にしてください。")]
    [Required(ErrorMessage = "タスク名は必須項目です。")]
    [BindProperty]
    public string TaskName { get; set; } = "";


    // 担当者
    [StringLength(50, ErrorMessage = "担当者名は50文字以内にしてください。")]
    [Required(ErrorMessage = "担当者名は必須項目です。")]
    [BindProperty]
    public string Assignee { get; set; } = "";


    // 期日
    [Required(ErrorMessage = "期日は必須項目です。")]
    [BindProperty]
    public DateTime DueDate { get; set; }


    // 進捗
    [BindProperty]
    public Status SelectedStatus { get; set; } = Status.NotStarted;

    // 進捗リスト
    public List<SelectListItem> ListItem { get; set; }

    // 進捗本体
    private readonly Dictionary<Status, string> _statusMap =
        new()
        {
            [Status.NotStarted] = "未着手",
            [Status.Processing] = "進行中",
            [Status.Completed] = "完了",
        };

    // コンストラクタ
    public EditModel(Repository repository)
    {
        _repository = repository;

        // リスト組立
        ListItem = [.. _statusMap.Select(x=>new SelectListItem { Value = x.Key.ToString(), Text=x.Value})];
    }


    // 画面表示
    public IActionResult OnGet(int id)
    {
        try
        {
            // DBからデータを取得する
            // 取得失敗は404
            var task = _repository.GetTargetTask(id) ??
                throw new KeyNotFoundException();

            // 各プロパティに格納
            TaskId = task.TASK_ID;
            TaskName = task.TASK_NAME;
            Assignee = task.ASSIGNEE;
            DueDate = task.DUE_DATE;
            SelectedStatus = _statusMap.FirstOrDefault(x=>x.Value == task.STATUS).Key;

            // 画面表示
            return Page();
        }
        catch (KeyNotFoundException)
        {
            // データ取得に失敗
            return NotFound($"タスクID:{id}は存在しません。");
        }
        catch (Exception ex)
        {
            // その他内部エラー
            System.Diagnostics.Debug.WriteLine(ex);
            return StatusCode(500, "内部エラーが発生したため、データを取得できませんでした。");
        }
    }

    // 更新ボタン
    public IActionResult OnPostUpdate(int id)
    {
        try
        {
            // バリデーションチェック
            // 失敗で再表示する（エラーつき）
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // 更新データ
            var newTask = new UpdateDto(id, TaskName, Assignee, DueDate, _statusMap[SelectedStatus]);

            // DBを更新
            // 更新失敗は404
            var affectRows = _repository.UpdateTask(newTask);

            if(affectRows == 0)
            {
                throw new KeyNotFoundException();
            }

            // 更新完了でリダイレクト
            return RedirectToPage("/Index");

        }
        catch (KeyNotFoundException)
        {
            // データ取得に失敗
            return NotFound($"タスクID:{id}は存在しません。");
        }
        catch (Exception ex)
        {
            // その他内部エラー
            System.Diagnostics.Debug.WriteLine(ex);
            return StatusCode(500, "内部エラーが発生したため、更新に失敗しました。");
        }
    }

    // 一覧画面への遷移は、aタグ遷移とする

}
