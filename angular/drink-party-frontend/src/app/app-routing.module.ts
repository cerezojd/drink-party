import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainComponent } from './views/main/main.component';
import { RoomComponent } from './views/room/room.component';

const routes: Routes = [
  {
    path: '',
    component: MainComponent,
  },
  {
    path: 'room/:roomCode',
    component: RoomComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
