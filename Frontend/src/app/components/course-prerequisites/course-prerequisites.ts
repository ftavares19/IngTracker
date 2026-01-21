import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, ActivatedRoute } from '@angular/router';
import { CourseService } from '../../services/course';
import { Course } from '../../models/course.model';

@Component({
  selector: 'app-course-prerequisites',
  imports: [CommonModule],
  templateUrl: './course-prerequisites.html',
  styleUrl: './course-prerequisites.css',
})
export class CoursePrerequisites implements OnInit {
  courseId!: number;
  course: Course | null = null;
  allCourses: Course[] = [];
  prerequisites: Course[] = [];
  availableCourses: Course[] = [];
  loading = false;
  error = '';

  constructor(
    private courseService: CourseService,
    private router: Router,
    private route: ActivatedRoute,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.courseId = +params['id'];
      this.loadData();
    });
  }

  loadData(): void {
    this.loading = true;
    this.error = '';
    console.log('🔍 Cargando datos para curso:', this.courseId);

    this.courseService.getCourseById(this.courseId).subscribe({
      next: (course) => {
        console.log('✅ Curso cargado:', course);
        this.course = course;
        this.cdr.detectChanges();
        this.loadCourses();
      },
      error: (err) => {
        console.error('❌ Error al cargar curso:', err);
        this.error = 'Error al cargar el curso';
        this.loading = false;
        this.cdr.detectChanges();
      }
    });
  }

  loadCourses(): void {
    console.log('📚 Cargando todas las materias...');
    this.courseService.getAllCourses().subscribe({
      next: (courses) => {
        console.log('✅ Materias cargadas:', courses.length);
        this.allCourses = courses;
        this.cdr.detectChanges();
        this.loadPrerequisites();
      },
      error: (err) => {
        console.error('❌ Error al cargar materias:', err);
        this.error = 'Error al cargar las materias';
        this.loading = false;
        this.cdr.detectChanges();
      }
    });
  }

  loadPrerequisites(): void {
    console.log('📋 Cargando prerrequisitos...');
    this.courseService.getPrerequisites(this.courseId).subscribe({
      next: (prereqs) => {
        console.log('✅ Prerrequisitos cargados:', prereqs);
        this.prerequisites = prereqs;
        this.updateAvailableCourses();
        this.loading = false;
        console.log('✅ Loading finalizado. Prerequisites:', this.prerequisites.length, 'Available:', this.availableCourses.length);
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error('❌ Error al cargar prerrequisitos:', err);
        this.error = 'Error al cargar los prerrequisitos';
        this.loading = false;
        this.cdr.detectChanges();
      }
    });
  }

  updateAvailableCourses(): void {
    const prereqIds = this.prerequisites.map(p => p.id);
    this.availableCourses = this.allCourses.filter(c => 
      c.id !== this.courseId && 
      !prereqIds.includes(c.id) &&
      c.degreeId === this.course?.degreeId
    );
  }

  addPrerequisite(prerequisiteId: number): void {
    this.courseService.addPrerequisite(this.courseId, prerequisiteId).subscribe({
      next: () => {
        this.loadData();
      },
      error: (err) => {
        this.error = 'Error al agregar prerrequisito';
        console.error(err);
      }
    });
  }

  removePrerequisite(prerequisiteId: number): void {
    if (confirm('¿Estás seguro de que quieres eliminar este prerrequisito?')) {
      this.courseService.removePrerequisite(this.courseId, prerequisiteId).subscribe({
        next: () => {
          this.loadData();
        },
        error: (err) => {
          this.error = 'Error al eliminar prerrequisito';
          console.error(err);
        }
      });
    }
  }

  getSemesterName(semester: number): string {
    return `Semestre ${semester + 1}`;
  }

  goBack(): void {
    this.router.navigate(['/courses']);
  }
}
