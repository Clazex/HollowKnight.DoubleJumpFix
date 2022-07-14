using System.Reflection;

using Modding;

namespace DoubleJumpFix;

public sealed class DoubleJumpFix : Mod {
	public override string GetVersion() => typeof(DoubleJumpFix)
		.Assembly
		.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
		.InformationalVersion;

	public override void Initialize() {
	}
}
