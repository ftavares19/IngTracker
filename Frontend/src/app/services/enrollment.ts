import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Enrollment, AddEnrollmentRequest, ModifyEnrollmentRequest } from '../models/enrollment.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class EnrollmentService {
  private apiUrl = `${environment.apiUrl}/enrollments`;

  constructor(private http: HttpClient) {}

  getAllEnrollments(): Observable<Enrollment[]> {
    return this.http.get<{enrollments: Enrollment[]}>(this.apiUrl).pipe(
      map(response => response.enrollments)
    );
  }

  getEnrollmentById(id: number): Observable<Enrollment> {
    return this.http.get<Enrollment>(`${this.apiUrl}/${id}`);
  }

  addEnrollment(request: AddEnrollmentRequest): Observable<Enrollment> {
    return this.http.post<Enrollment>(this.apiUrl, request);
  }

  modifyEnrollment(id: number, request: ModifyEnrollmentRequest): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, request);
  }

  deleteEnrollment(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
