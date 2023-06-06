import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: 'translators', loadChildren: () => import('./translator/translator.module').then(m => m.TranslatorModule) },
  { path: '', redirectTo: 'translators', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }