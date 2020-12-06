import { Component, OnInit } from '@angular/core';
import { Player } from 'src/app/models/player';
import { GameService } from 'src/app/services/game.service';

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.scss'],
})
export class RoomComponent implements OnInit {
  player: Player;

  constructor(private gameService: GameService) {}

  ngOnInit(): void {
    this.player = this.gameService.player;
  }
}
