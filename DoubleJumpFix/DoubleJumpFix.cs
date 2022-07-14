using System.Reflection;

using Modding;

using Mono.Cecil.Cil;

using MonoMod.Cil;

namespace DoubleJumpFix;

public sealed class DoubleJumpFix : Mod {
	public override string GetVersion() => typeof(DoubleJumpFix)
		.Assembly
		.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
		.InformationalVersion;

	public override void Initialize() =>
		IL.HeroController.JumpReleased += Fix;

	private static void Fix(ILContext il) {
		ILLabel label = new ILCursor(il).GotoNext(MoveType.After, i => i.MatchBlt(out _)).MarkLabel();
		_ = new ILCursor(il)
			.GotoNext(MoveType.After, i => i.MatchBleUn(out _))
			.Emit(OpCodes.Ldarg_0)
			.Emit(OpCodes.Ldfld, ReflectionHelper.GetFieldInfo(typeof(HeroController), nameof(HeroController.cState)))
			.Emit(OpCodes.Ldfld, ReflectionHelper.GetFieldInfo(typeof(HeroControllerStates), nameof(HeroControllerStates.doubleJumping)))
			.Emit(OpCodes.Brtrue_S, label);
	}
}
