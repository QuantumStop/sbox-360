namespace Editor;

partial class SceneEditorSession
{
	/// <summary>
	/// The game session of this editor session, if playing.
	/// </summary>
	public GameEditorSession GameSession { get; private set; }

	public virtual bool IsPlaying => GameSession != null;

	public void SetPlaying( Scene scene )
	{
		GameSession = new GameEditorSession( this, scene );
		GameSession.MakeActive();
	}

	public virtual void StopPlaying()
	{
		GameSession?.Destroy();
		GameSession = null;

		MakeActive();
	}
}
