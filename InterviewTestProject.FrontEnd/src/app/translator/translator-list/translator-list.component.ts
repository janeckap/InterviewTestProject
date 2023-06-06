import { Component, OnInit } from '@angular/core';
import { TranslatorService } from '../translator.service';
import { TranslatorModel } from '../translator.model';

@Component({
  selector: 'app-translator-list',
  templateUrl: './translator-list.component.html',
  styleUrls: ['./translator-list.component.css']
})
export class TranslatorListComponent implements OnInit {
  translators: TranslatorModel[];

  constructor(private translatorService: TranslatorService) {}

  ngOnInit(): void {
    this.loadTranslators();
  }

  loadTranslators(): void {
    this.translatorService.getTranslators().subscribe({
      next: (translators) => this.translators = translators
    });
  }
}