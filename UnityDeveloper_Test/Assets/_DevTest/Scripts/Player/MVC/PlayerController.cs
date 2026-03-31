using UnityEngine;

namespace DevTest.Player
{
    public class PlayerController
    {
        public PlayerModel PlayerModel { get; }
        public PlayerView PlayerView { get; }

        public PlayerController(PlayerModel playerModel, PlayerView playerView, Transform spawnPoint)
        {
            PlayerModel = playerModel;
            PlayerView = Object.Instantiate(playerView, spawnPoint);




            // breaking one-way control to allow PlayerController functions to be run from the PlayerView
            PlayerView.PlayerController = this;
        }
    }

}