import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TranslatorModel } from './translator.model';

@Injectable({
  providedIn: 'root'
})
export class TranslatorService {
  private baseUrl = 'http://localhost:5000/api/translators';

  constructor(private http: HttpClient) {}

  getTranslators(): Observable<TranslatorModel[]> {
    return this.http.get<TranslatorModel[]>(this.baseUrl);
  }

  getTranslatorsByName(name: string): Observable<TranslatorModel[]> {
    return this.http.get<TranslatorModel[]>(`${this.baseUrl}/name/${name}`);
  }

  getTranslator(id: number): Observable<TranslatorModel> {
    return this.http.get<TranslatorModel>(`${this.baseUrl}/${id}`);
  }

  createTranslator(translator: TranslatorModel): Observable<TranslatorModel> {
    return this.http.post<TranslatorModel>(this.baseUrl, translator);
  }

  updateTranslatorStatus(id: number, newStatus: string): Observable<TranslatorModel> {
    return this.http.put<TranslatorModel>(`${this.baseUrl}/${id}/status?newStatus=${newStatus}`, null);
  }
}