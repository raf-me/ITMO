from sqlalchemy import Integer, String, Date, ForeignKey, CheckConstraint
from sqlalchemy.orm import Mapped, mapped_column, relationship
from .db import Base


class Teacher(Base):
    __tablename__ = "teachers"

    id: Mapped[int] = mapped_column(Integer, primary_key=True)
    fio: Mapped[str] = mapped_column(String(100), nullable=False)

    classrooms = relationship("Classroom", back_populates="teacher", cascade="all,delete")


class Classroom(Base):
    __tablename__ = "classrooms"

    id: Mapped[int] = mapped_column(Integer, primary_key=True)
    name: Mapped[str] = mapped_column(String(20), nullable=False)
    teacher_id: Mapped[int] = mapped_column(ForeignKey("teachers.id"), nullable=False)
    students_count: Mapped[int] = mapped_column(Integer, nullable=False)

    __table_args__ = (
        CheckConstraint("students_count >= 0 AND students_count <= 40", name="chk_students_count"),
    )

    teacher = relationship("Teacher", back_populates="classrooms")
    students = relationship("Student", back_populates="classroom", cascade="all,delete")


class Student(Base):
    __tablename__ = "students"

    id: Mapped[int] = mapped_column(Integer, primary_key=True)
    fio: Mapped[str] = mapped_column(String(100), nullable=False)
    birth_date: Mapped[str] = mapped_column(String(10), nullable=False)
    rating_pos: Mapped[int] = mapped_column(Integer, nullable=False)

    classroom_id: Mapped[int] = mapped_column(ForeignKey("classrooms.id"), nullable=False)
    classroom = relationship("Classroom", back_populates="students")