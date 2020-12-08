import { Component, OnInit } from '@angular/core';
import { Player } from 'src/app/models/player';
import { GameSignalRService } from 'src/app/services/game-signalr.service';
import { GameService } from 'src/app/services/game.service';

export interface PlayerOutput {
  id: string;
  name: string;
  isAdmin: boolean;
}

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.scss'],
})
export class RoomComponent implements OnInit {
  me: PlayerOutput;
  currentId: string;
  players: PlayerOutput[] = [];
  events: string[];

  constructor(
    private gameService: GameService,
    private gameSignalRService: GameSignalRService
  ) {}

  ngOnInit(): void {
    this.currentId = this.gameService.getCurrentId();
    this.gameSignalRService.configureHub(this.gameService.getStoredToken());
    this.gameSignalRService.startConnection();

    this.gameSignalRService.getPlayers().subscribe((result) => {
      this.me = result.find((p) => p.id === this.currentId);
      this.players = result.filter((p) => p.id !== this.currentId);
    });
  }

  private handlePlayers(players: PlayerOutput[]) {}
}
