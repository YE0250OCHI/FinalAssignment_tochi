using Assignment1_TodoApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Assignment1_TodoApp.Pages;

public class IndexModel : PageModel
{
    // DB操作部
    private readonly Repository _repository;

    // タスク一覧
    public List<TaskModel> Tasks { get; set; } = [];

    // 担当検索
    [BindProperty(SupportsGet = true)]
    public string FilterAssignee { get; set; } = "";

    // 進捗フィルタ
    [BindProperty(SupportsGet = true)]
    public FilterMode SelectedStatus { get; set; } = FilterMode.All;

    // フィルタ用リスト
    public List<SelectListItem> ItemList { get; set; }

    // フィルタ本体
    private readonly Dictionary<FilterMode, string> _filterMap =
        new()
        {
            [FilterMode.All] = "全て",
            [FilterMode.NotStarted] = "未着手",
            [FilterMode.Processing] = "進行中",
            [FilterMode.Completed] = "完了",
        };

    // コンストラクタ
    public IndexModel(Repository repository)
    {
        _repository = repository;

        ItemList = [.. _filterMap.Select(x=>new SelectListItem { Value = x.Key.ToString(), Text = x.Value})];
    }


    // 一覧表示
    public IActionResult OnGet()
    {
        try
        {
            // データ取得
            var temp = _repository.GetTasks();

            // 担当者フィルターの適用
            if (!string.IsNullOrWhiteSpace(FilterAssignee))
            {
                temp = [.. temp.Where(x => x.ASSIGNEE == FilterAssignee)];
            }

            // 進捗フィルターを適用する
            if (SelectedStatus == FilterMode.All)
            {
                Tasks = temp;
            }
            else
            {
                var filter = _filterMap[SelectedStatus];

                Tasks = [.. temp.Where(x => x.STATUS == filter)];
            }

            // 画面表示
            return Page();
        }
        catch (Exception ex)
        {
            // その他内部エラー
            System.Diagnostics.Debug.WriteLine(ex);
            return StatusCode(500, "内部エラーが発生したため、データを取得できませんでした。");
        }    
    }

    // 更新画面への遷移は、aタグ遷移とする

}

