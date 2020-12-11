import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { SelectGameModePage } from './select-gm.page';

const routes: Routes = [
  {
    path: '',
    component: SelectGameModePage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class SelectGameModePageRoutingModule {}
