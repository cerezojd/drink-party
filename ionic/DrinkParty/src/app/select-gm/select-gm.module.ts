import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { SelectGameModePageRoutingModule } from './select-gm-routing.module';

import { SelectGameModePage } from './select-gm.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    SelectGameModePageRoutingModule
  ],
  declarations: [SelectGameModePage]
})
export class SelectGameModePageModule {}
