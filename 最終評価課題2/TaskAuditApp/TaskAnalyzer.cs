using NLog; // using追加

namespace TaskAuditApp
{
    public class TaskAnalyzer(Logger logger) // プライマリコンストラクタ化
    {
        /// <summary>
        /// 期限切れの未完了タスクをチェックし、警告ログを出力する
        /// </summary>
        public void CheckOverdueTasks(List<TaskModel> tasks)
        {
            // プライマリコンストラクタの引数をそのまま利用
            logger.Info("期限切れタスクのチェックを開始します。");

            foreach (var task in tasks)
            {
                // ステータスが完了以外を対象とする
                if (task.STATUS != "完了")
                {
                    // 追加：未完了タスクを表示
                    // 通常タスクも抽出する
                    logger.Info($"[タスクID:{task.TASK_ID}] タスク：{task.TASK_NAME} 担当: {task.ASSIGNEE}, 期限: {task.DUE_DATE:yyyy/MM/dd} 進捗状況:{task.STATUS}");

                    // 期限日の判定
                    // 判定が逆　-> DueDateがTodaより小さい（過去）を拾う
                    if (task.DUE_DATE < DateTime.Today)
                    {
                        // 警告ログ出力

                        // プライマリコンストラクタの引数をそのまま利用
                        // Warnに変更
                        // \文字削除
                        logger.Warn($"【警告】期限切れタスクを発見: {task.TASK_NAME} (担当: {task.ASSIGNEE}, 期限: {task.DUE_DATE:yyyy/MM/dd})");

                    }
                }
            }
        }


        /// <summary>
        /// 担当者ごとの未完了タスク数を集計してログ出力する
        /// </summary>

        public void LogTaskCountByAssignee(List<TaskModel> tasks)
        {
            // プライマリコンストラクタの引数をそのまま利用
            logger.Info("担当者ごとの未完了タスク数集計を出力します。");

            // 担当者ごとの未完了リスト            
            List<string> assigneeList = [.. 
                tasks
                    .Where(x => x.STATUS != "完了")
                    .OrderBy(x => x.ASSIGNEE)
                    .GroupBy(x => x.ASSIGNEE)
                    .Select(x => x.Key)];
            // 1. Whereで、未完了タスク（完了タスクではないもの）に絞る
            // 2. OrderByで、担当者名順にする
            // 3. GroupByで、担当者ごとのまとまりに加工
            // 2. Selectで、上記まとまりをキー（担当者名）のリストに加工

            // 集計
            foreach (var assignee in assigneeList)
            {
                // LINQでの集計
                int count =
                    tasks
                        .Where(x => x.ASSIGNEE == assignee && x.STATUS != "完了")
                        .Count();
                // 1. Whereで担当者かつ、完了ではないものに絞る
                // 2. Countで集計する

                // プライマリコンストラクタの引数をそのまま利用
                // \文字削除
                logger.Info($"担当者: {assignee} / 未完了タスク数: {count}件");
            }            
        }

    }

}

