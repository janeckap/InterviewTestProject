import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TranslatorRoutingModule } from './translator-routing.module';
import { TranslatorListComponent } from './translator-list/translator-list.component';
import { CreateTranslatorComponent } from './create-translator/create-translator.component';
import { UpdateStatusComponent } from './update-status/update-status.component';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    TranslatorListComponent,
    CreateTranslatorComponent,
    UpdateStatusComponent
  ],
  imports: [
    CommonModule,
    TranslatorRoutingModule,
    FormsModule
  ]
})
export class TranslatorModule { }
