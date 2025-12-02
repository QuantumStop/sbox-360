using Sandbox.Bind;

namespace Editor
{
	public partial class Widget : QObject
	{


	}
}

namespace Sandbox
{
	public static partial class SandboxToolExtensions
	{
		/// <summary>
		/// Bind the Left hand side to the value of the given console variable.
		/// </summary>
		public static Link FromConsoleVariable( this Builder self, string name )
		{
			var c = self;
			return c.From( () => ConsoleSystem.GetValue( name ), x => ConsoleSystem.SetValue( name, x ) );
		}

		/// <summary>
		/// Bind the Left hand side to the value of the given console variable as an integer.
		/// </summary>
		public static Link FromConsoleVariableInt( this Builder self, string name )
		{
			var c = self;
			return c.From( () => Editor.ConsoleSystem.GetValueInt( name, 0 ), x => ConsoleSystem.SetValue( name, x ) );
		}
	}
}


public static class WidgetExtensions
{
	public static IEnumerable<T> FindAllChildren<T>( this Widget widget ) where T : Widget
	{
		foreach ( var child in widget.Children )
		{
			if ( child is T t )
				yield return t;

			// Recurse into children
			foreach ( var nested in child.FindAllChildren<T>() )
				yield return nested;
		}
	}

	public static T FindParent<T>( this Widget widget ) where T : Widget
	{
		var parent = widget.Parent;

		while ( parent != null )
		{
			if ( parent is T t )
				return t;

			parent = parent.Parent;
		}

		return null;
	}
}
