using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace ProjectRGame
{
    public class InputManager
    {
        private KeyboardState _currentState;
        private KeyboardState _oldState;

        public void update(KeyboardState state)
        {
            _oldState = _currentState;
            _currentState = state;
        }

        public bool keyPressed(Keys key)
        {
            return _currentState.IsKeyDown(key);
        }

        public bool keyTyped(Keys key)
        {
            return (!_oldState.IsKeyDown(key) && (_currentState.IsKeyDown(key)));
        }
    }
}
