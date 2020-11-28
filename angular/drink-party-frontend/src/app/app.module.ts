import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { MainComponent } from './views/main/main.component';
import { JoinFormComponent } from './views/main/join-form/join-form.component';
import { CreateFormComponent } from './views/main/create-form/create-form.component';
import { RoomComponent } from './views/room/room.component';

@NgModule({
  declarations: [
    AppComponent,
    MainComponent,
    JoinFormComponent,
    CreateFormComponent,
    RoomComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    ReactiveFormsModule,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
