using System.Collections.Generic;
using UnityEngine;


public class StateMachine
{
    private List<State> states = new List<State>();
    private State currentState;

    public void ChangeToState(string stateName)
    {
        // Si existe un currentState llama a su exit
        currentState?.Exit();

        // Busca el state que le pasamos por parametro
        currentState = states.Find(x => x.StateName == stateName);

        // Si no lo encuentra tira error y return
        if (currentState is null)
        {
            Debug.LogError($"State {stateName} cannot be found.");
            return;
        }

        // Si lo encontro llama a su enter
        currentState.Enter();
    }

    public void Add(State state)
    {
        states.Add(state);
    }

    public void Update()
    {
        currentState.Update();
    }

    public void FixedUpdate()
    {
        currentState.FixedUpdate();
    }
}