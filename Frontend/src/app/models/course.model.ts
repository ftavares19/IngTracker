export enum Semester {
  Semester1 = 0,
  Semester2 = 1,
  Semester3 = 2,
  Semester4 = 3,
  Semester5 = 4,
  Semester6 = 5,
  Semester7 = 6,
  Semester8 = 7
}

export interface Course {
  id?: number;
  code: string;
  name: string;
  semester: Semester;
  degreeId: number;
}

export interface AddCourseRequest {
  code: string;
  name: string;
  semester: Semester;
  degreeId: number;
}

export interface ModifyCourseRequest {
  code: string;
  name: string;
  semester: Semester;
  degreeId: number;
}
