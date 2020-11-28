import { Component } from '@angular/core';
import { GameSignalRService } from './services/game-signalr.service';

@Component({
  selector: 'app-root',
  template: `<router-outlet></router-outlet>`,
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'drink-party';

  constructor(private gameService: GameSignalRService) {
    this.gameService.startConnection();
  }
}
