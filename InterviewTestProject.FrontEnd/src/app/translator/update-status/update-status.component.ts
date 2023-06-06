import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslatorModel } from '../translator.model';
import { TranslatorService } from '../translator.service';

@Component({
  selector: 'app-update-status',
  templateUrl: './update-status.component.html',
  styleUrls: ['./update-status.component.css']
})
export class UpdateStatusComponent implements OnInit {
  translator: TranslatorModel = {
    id: 0,
    name: '',
    hourlyRate: 0,
    status: '',
    creditCardNumber: ''
  };
  newStatus: string;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private translatorService: TranslatorService,
  ) { }

  ngOnInit(): void {
    const translatorId = Number(this.route.snapshot.paramMap.get('id'))
    this.getTranslator(translatorId);
  }

  getTranslator(id: number): void {
    this.translatorService.getTranslator(id).subscribe({
      next: (translator) => {
        this.translator = translator;
        this.newStatus = translator.status;
      }
    });
  }

  updateTranslatorStatus(): void {
    this.translatorService.updateTranslatorStatus(this.translator.id, this.newStatus).subscribe({
      next: () => this.router.navigate(['/translators'])
    });
  }
}