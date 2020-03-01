using System;

namespace Playground.StateMachine.States.Register
{
    public class RegisterCustomAction: IEquatable<RegisterCustomAction>
    {
        public RegisterFormTrigger Trigger { get; }

        public int  RandomNumber { get; }

        public RegisterCustomAction(RegisterFormTrigger trigger, int randomNumber)
        {
            Trigger = trigger;
            RandomNumber = randomNumber;
        }

        /// <inheritdoc />
        public bool Equals(RegisterCustomAction other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Trigger == other.Trigger && RandomNumber == other.RandomNumber;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (!(obj is RegisterCustomAction action)) return false;
            return Equals(action);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return Trigger.GetHashCode() ^ RandomNumber.GetHashCode();
            }
        }
    }
}