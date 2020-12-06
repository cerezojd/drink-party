import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  RouterStateSnapshot,
} from '@angular/router';
import { GameService } from '../services/game.service';

@Injectable({ providedIn: 'root' })
export class RoomGuard implements CanActivate {
  constructor(private gameService: GameService) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return true;
  }
}
