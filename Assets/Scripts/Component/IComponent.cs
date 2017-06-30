using System;

namespace RacingHuntZombie {
	public interface IComponent {

		void StartComponent();
		void UpdateComponent(float dt);
		void EndComponent();

	}
}
