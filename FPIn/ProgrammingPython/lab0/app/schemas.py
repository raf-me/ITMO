from datetime import date
from pydantic import BaseModel, Field, field_validator


class ErrorOut(BaseModel):
    status: int
    reason: str


class TeacherIn(BaseModel):
    fio: str = Field(..., max_length=100)

    @field_validator("fio")
    @classmethod
    def fio_not_empty(cls, v: str):
        if not v.strip():
            raise ValueError("Field 'fio' is required")
        return v


class TeacherOut(TeacherIn):
    id: int


class ClassroomIn(BaseModel):
    name: str = Field(..., max_length=20)
    teacher_id: int
    students_count: int = Field(..., ge=0, le=40)

    @field_validator("name")
    @classmethod
    def name_not_empty(cls, v: str):
        if not v.strip():
            raise ValueError("Field 'name' is required")
        return v


class ClassroomOut(ClassroomIn):
    id: int


class StudentIn(BaseModel):
    fio: str = Field(..., max_length=100)
    birth_date: date
    rating_pos: int = Field(..., ge=1)
    classroom_id: int

    @field_validator("fio")
    @classmethod
    def fio_not_empty(cls, v: str):
        if not v.strip():
            raise ValueError("Field 'fio' is required")
        return v


class StudentOut(BaseModel):
    id: int
    fio: str
    birth_date: date
    rating_pos: int
    classroom_id: int