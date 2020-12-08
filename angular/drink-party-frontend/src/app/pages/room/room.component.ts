import { Component, OnInit } from '@angular/core';
import { GameModeType } from 'src/app/models/main';
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

  modes = GameModeType;

  gameMode: GameModeType;
  started: boolean;

  constructor(
    private gameService: GameService,
    private gameSignalRService: GameSignalRService
  ) {}

  ngOnInit(): void {
    this.currentId = this.gameService.getCurrentId();
    this.gameSignalRService.configureHub(this.gameService.getStoredToken());
    this.gameSignalRService.startConnection();

    // SignalR events
    this.subscribeToPlayers();
    this.subscribeToGameInfo();
  }

  private subscribeToPlayers() {
    this.gameSignalRService.getPlayers().subscribe((result) => {
      console.log(result);
      this.me = result.find((p) => p.id === this.currentId);
      this.players = result.filter((p) => p.id !== this.currentId);
    });
  }

  private subscribeToGameInfo() {
    this.gameSignalRService.getGameInfo().subscribe((result) => {
      this.gameMode = result.gameMode;
      this.started = result.started;
      console.log('Game info', result);
    });
  }

  onSelectMode(mode: GameModeType) {
    this.gameSignalRService.chooseGameMode(mode).subscribe((_) => {
      console.log('Selecciono el modo', mode);
    });
  }
}
