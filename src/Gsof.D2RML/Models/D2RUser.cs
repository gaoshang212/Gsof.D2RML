using LiteDB;

namespace Gsof.D2RML.Models;

//"D:\Games\Diablo II Resurrected\D2R.exe" -username xxxx -password  "xxxx" -address kr.actual.battle.net  -mod wing

public class D2RUser
{
    public int Id { get; set; }

    /// <summary>
    /// 是否运行
    /// </summary>
    public bool IsLaunch { get; set; } = false;

    /// <summary>
    /// 用户名
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// 服务器地址
    /// </summary>
    public string? Address { get; set; } = "kr.actual.battle.net";

    /// <summary>
    /// Mod 以空格切分
    /// </summary>
    public string? Mod { get; set; }

    /// <summary>
    /// 额外的命令行参数
    /// </summary>
    public string? Extra { get; set; }

}