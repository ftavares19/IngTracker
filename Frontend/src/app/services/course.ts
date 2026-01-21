import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Course, AddCourseRequest, ModifyCourseRequest } from '../models/course.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class CourseService {
  private apiUrl = `${environment.apiUrl}/courses`;

  constructor(private http: HttpClient) {}

  getAllCourses(): Observable<Course[]> {
    return this.http.get<{courses: Course[]}>(this.apiUrl).pipe(
      map(response => response.courses)
    );
  }

  getCourseById(id: number): Observable<Course> {
    return this.http.get<Course>(`${this.apiUrl}/${id}`);
  }

  addCourse(request: AddCourseRequest): Observable<Course> {
    return this.http.post<Course>(this.apiUrl, request);
  }

  modifyCourse(id: number, request: ModifyCourseRequest): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, request);
  }

  deleteCourse(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  addPrerequisite(courseId: number, prerequisiteId: number): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/${courseId}/prerequisites`, { prerequisiteId });
  }

  removePrerequisite(courseId: number, prerequisiteId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${courseId}/prerequisites/${prerequisiteId}`);
  }

  getPrerequisites(courseId: number): Observable<Course[]> {
    return this.http.get<{prerequisites: Course[]}>(`${this.apiUrl}/${courseId}/prerequisites`).pipe(
      map(response => response.prerequisites)
    );
  }
}
