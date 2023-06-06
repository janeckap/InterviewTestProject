import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TranslatorListComponent } from './translator-list/translator-list.component';
import { CreateTranslatorComponent } from './create-translator/create-translator.component';
import { UpdateStatusComponent } from './update-status/update-status.component';

const routes: Routes = [
  { path: '', component: TranslatorListComponent },
  { path: 'create', component: CreateTranslatorComponent },
  { path: 'update-status/:id', component: UpdateStatusComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TranslatorRoutingModule { }
