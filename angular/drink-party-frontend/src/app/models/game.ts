export interface Player {
  connectionId: string;
  name: string;
  isAdmin: boolean;
}

export interface GameInfo {
  players: Player[];
  roomCode: string;
  started: boolean;
}
