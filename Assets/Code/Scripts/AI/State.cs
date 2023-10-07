public abstract class State
{
    public string StateName;

    protected Enemy Enemy;

    public State(Enemy enemy, string stateName)
    {
        Enemy = enemy;
        StateName = stateName;
    }

    public virtual void Update()
    {
    }

    public virtual void FixedUpdate()
    {
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }
}