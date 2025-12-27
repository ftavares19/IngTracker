import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Degree, AddDegreeRequest, ModifyDegreeRequest } from '../models/degree.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class DegreeService {
  private apiUrl = `${environment.apiUrl}/degrees`;

  constructor(private http: HttpClient) {}

  getAllDegrees(): Observable<Degree[]> {
    return this.http.get<Degree[]>(this.apiUrl);
  }

  getDegreeById(id: number): Observable<Degree> {
    return this.http.get<Degree>(`${this.apiUrl}/${id}`);
  }

  addDegree(request: AddDegreeRequest): Observable<Degree> {
    return this.http.post<Degree>(this.apiUrl, request);
  }

  modifyDegree(id: number, request: ModifyDegreeRequest): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, request);
  }

  deleteDegree(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
