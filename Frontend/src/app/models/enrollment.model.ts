export enum Status {
  Pending = 0,
  InProgress = 1,
  Passed = 2
}

export interface Enrollment {
  id?: number;
  courseId: number;
  status: Status;
  grade?: number;
  approvalDate?: Date;
}

export interface AddEnrollmentRequest {
  courseId: number;
  status: Status;
  grade?: number;
  approvalDate?: Date;
}

export interface ModifyEnrollmentRequest {
  courseId: number;
  status: Status;
  grade?: number;
  approvalDate?: Date;
}
