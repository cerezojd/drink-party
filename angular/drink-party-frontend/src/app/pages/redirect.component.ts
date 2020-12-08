import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GameService } from '../services/game.service';

@Component({
  selector: 'app-redirect',
  template: ``,
})
export class RedirectComponent implements OnInit {
  constructor(private gameService: GameService, private router: Router) {}

  ngOnInit() {
    const token = this.gameService.getStoredToken();
    if (!token) {
      this.router.navigate(['/main']);
      return;
    }

    // this.gameService.setCurrentSession(token);
    // const player = this.gameService.player;
    // this.router.navigate(['/room', player.roomCode]);
  }
}
