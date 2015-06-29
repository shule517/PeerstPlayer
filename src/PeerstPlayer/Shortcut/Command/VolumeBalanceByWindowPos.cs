using PeerstPlayer.Controls.PecaPlayer;

namespace PeerstPlayer.Shortcut.Command
{
    /// <summary>
    /// 自動バランス調整切り替えコマンド
    /// </summary>
    class VolumeBalanceByWindowPosCommand : IShortcutCommand
    {
        private PecaPlayerControl pecaPlayer;

        public VolumeBalanceByWindowPosCommand(PecaPlayerControl pecaPlayer)
        {
            this.pecaPlayer = pecaPlayer;
        }

        public void Execute(CommandArgs commandArgs)
        {
            pecaPlayer.VolumeBalanceByWindowPos = !pecaPlayer.VolumeBalanceByWindowPos;
        }

        string IShortcutCommand.GetDetail(CommandArgs commandArgs)
        {
            return "音量バランス：ウィンドウ位置に応じて変更";
        }
    }
}
