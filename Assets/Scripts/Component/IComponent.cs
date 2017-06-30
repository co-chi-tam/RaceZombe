using System;

namespace RacingHuntZombie {
	public interface IComponent {

		void Init ();
		void StartComponent ();
		void UpdateComponent (float dt);
		void EndComponent ();

	}
}
