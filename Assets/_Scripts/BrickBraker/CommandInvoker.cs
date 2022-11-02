using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CommandInvoker : MonoBehaviour
{
    PlayerAction _playerAction;

    static Queue<iCommand> commandBuffer;

    static List<iCommand> commandHistory;

    static int counter;

    private void Start()
    {
        commandBuffer = new Queue<iCommand> ();
        commandHistory = new List<iCommand> ();

        _playerAction = PlayerInputController.Instance._playerAction;

        _playerAction.PlayerControl.UndoBrake.performed += context => UndoCommand();
    }

    public static void AddCommand(iCommand command)
    {
        
        while (commandHistory.Count > counter)
        {
            commandHistory.RemoveAt(counter);
        }

        commandBuffer.Enqueue(command);


    }

    private void UndoCommand()
    {
        if (commandBuffer.Count <= 0)
        {
            if (counter > 0)
            {
                counter--;
                commandHistory[counter].Undo();
            }
        }
    }

    private void Update()
    {
        if (commandBuffer.Count > 0)
        {
            iCommand c = commandBuffer.Dequeue();
            c.Execute();

            commandHistory.Add(c);

            counter++;

            Debug.Log("Command history length is: " + commandHistory.Count);
        }
    }
}
