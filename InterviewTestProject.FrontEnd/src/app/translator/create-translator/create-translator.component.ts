import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { TranslatorModel } from '../translator.model';
import { TranslatorService } from '../translator.service';

@Component({
  selector: 'app-create-translator',
  templateUrl: './create-translator.component.html',
  styleUrls: ['./create-translator.component.css']
})
export class CreateTranslatorComponent {
  newTranslator: TranslatorModel = {
    id: 0,
    name: '',
    hourlyRate: 0,
    status: '',
    creditCardNumber: ''
  };

  constructor(
    private router: Router,
    private translatorService: TranslatorService) { }

  createTranslator(): void {
    this.translatorService.createTranslator(this.newTranslator).subscribe({
      next: () => this.router.navigate(['/translators'])
    });
  }
}