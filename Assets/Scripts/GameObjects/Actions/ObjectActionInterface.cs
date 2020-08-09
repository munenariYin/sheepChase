// キャラの行動は全てここから継承される
// 引数のActionStatusを元に動く。本当に動くだけ
public interface ObjectActionInterface
{
    void Init();
    void Action(ActionInternalStatus actionStatus, ActionExternalStatus externalStatus);
}
