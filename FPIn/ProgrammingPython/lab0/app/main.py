from fastapi import FastAPI, Depends
from sqlalchemy.orm import Session
from sqlalchemy.exc import SQLAlchemyError

from .db import Base, engine, get_db
from .models import Teacher, Classroom, Student
from .schemas import (
    TeacherIn, TeacherOut,
    ClassroomIn, ClassroomOut,
    StudentIn, StudentOut,
)
from .errors import bad_request, not_found, internal_error

Base.metadata.create_all(bind=engine)

app = FastAPI(title="School API")


def unwrap_detail(e: Exception) -> str:
    msg = str(e)
    return msg if msg else "Unknown error"


@app.get("/api/teachers", response_model=dict)
def list_teachers(db: Session = Depends(get_db)):
    items = db.query(Teacher).all()
    return {"list": [{"id": t.id, "fio": t.fio} for t in items]}


@app.get("/api/teachers/{teacher_id}", response_model=dict)
def get_teacher(teacher_id: int, db: Session = Depends(get_db)):
    t = db.get(Teacher, teacher_id)
    if not t:
        not_found()
    return {"teacher": {"id": t.id, "fio": t.fio}}


@app.post("/api/teachers", response_model=dict)
def create_teacher(payload: dict, db: Session = Depends(get_db)):
    if "teacher" not in payload:
        bad_request("Field 'teacher' is required")
    try:
        data = TeacherIn(**payload["teacher"])
    except Exception as e:
        bad_request(unwrap_detail(e))

    try:
        t = Teacher(fio=data.fio)
        db.add(t)
        db.commit()
        db.refresh(t)
        return {"teacher": {"id": t.id, "fio": t.fio}}
    except SQLAlchemyError as e:
        db.rollback()
        internal_error(str(e))


@app.patch("/api/teachers/{teacher_id}", response_model=dict)
def patch_teacher(teacher_id: int, payload: dict, db: Session = Depends(get_db)):
    t = db.get(Teacher, teacher_id)
    if not t:
        not_found()
    if "teacher" not in payload:
        bad_request("Field 'teacher' is required")

    data = payload["teacher"]
    if "fio" in data:
        if not str(data["fio"]).strip():
            bad_request("Field 'fio' is required")
        if len(str(data["fio"])) > 100:
            bad_request("Field 'fio' max length is 100")
        t.fio = data["fio"]

    try:
        db.commit()
        db.refresh(t)
        return {"teacher": {"id": t.id, "fio": t.fio}}
    except SQLAlchemyError as e:
        db.rollback()
        internal_error(str(e))


@app.delete("/api/teachers/{teacher_id}", status_code=202)
def delete_teacher(teacher_id: int, db: Session = Depends(get_db)):
    t = db.get(Teacher, teacher_id)
    if not t:
        not_found()
    try:
        db.delete(t)
        db.commit()
        return {"status": 202}
    except SQLAlchemyError as e:
        db.rollback()
        internal_error(str(e))


@app.get("/api/classes", response_model=dict)
def list_classes(db: Session = Depends(get_db)):
    items = db.query(Classroom).all()
    return {"list": [{"id": c.id, "name": c.name, "teacher_id": c.teacher_id, "students_count": c.students_count} for c in items]}


@app.get("/api/classes/{class_id}", response_model=dict)
def get_class(class_id: int, db: Session = Depends(get_db)):
    c = db.get(Classroom, class_id)
    if not c:
        not_found()
    return {"classroom": {"id": c.id, "name": c.name, "teacher_id": c.teacher_id, "students_count": c.students_count}}


@app.post("/api/classes", response_model=dict)
def create_class(payload: dict, db: Session = Depends(get_db)):
    if "classroom" not in payload:
        bad_request("Field 'classroom' is required")
    try:
        data = ClassroomIn(**payload["classroom"])
    except Exception as e:
        bad_request(unwrap_detail(e))

    if not db.get(Teacher, data.teacher_id):
        bad_request("Field 'teacher_id' references missing teacher")

    try:
        c = Classroom(name=data.name, teacher_id=data.teacher_id, students_count=data.students_count)
        db.add(c)
        db.commit()
        db.refresh(c)
        return {"classroom": {"id": c.id, "name": c.name, "teacher_id": c.teacher_id, "students_count": c.students_count}}
    except SQLAlchemyError as e:
        db.rollback()
        internal_error(str(e))


@app.patch("/api/classes/{class_id}", response_model=dict)
def patch_class(class_id: int, payload: dict, db: Session = Depends(get_db)):
    c = db.get(Classroom, class_id)
    if not c:
        not_found()
    if "classroom" not in payload:
        bad_request("Field 'classroom' is required")

    data = payload["classroom"]

    if "name" in data:
        name = str(data["name"])
        if not name.strip():
            bad_request("Field 'name' is required")
        if len(name) > 20:
            bad_request("Field 'name' max length is 20")
        c.name = name

    if "teacher_id" in data:
        tid = int(data["teacher_id"])
        if not db.get(Teacher, tid):
            bad_request("Field 'teacher_id' references missing teacher")
        c.teacher_id = tid

    if "students_count" in data:
        sc = int(data["students_count"])
        if sc < 0 or sc > 40:
            bad_request("Field 'students_count' must be between 0 and 40")
        c.students_count = sc

    try:
        db.commit()
        db.refresh(c)
        return {"classroom": {"id": c.id, "name": c.name, "teacher_id": c.teacher_id, "students_count": c.students_count}}
    except SQLAlchemyError as e:
        db.rollback()
        internal_error(str(e))


@app.delete("/api/classes/{class_id}", status_code=202)
def delete_class(class_id: int, db: Session = Depends(get_db)):
    c = db.get(Classroom, class_id)
    if not c:
        not_found()
    try:
        db.delete(c)
        db.commit()
        return {"status": 202}
    except SQLAlchemyError as e:
        db.rollback()
        internal_error(str(e))


@app.get("/api/students", response_model=dict)
def list_students(db: Session = Depends(get_db)):
    items = db.query(Student).all()
    return {"list": [{"id": s.id, "fio": s.fio, "birth_date": s.birth_date, "rating_pos": s.rating_pos, "classroom_id": s.classroom_id} for s in items]}


@app.get("/api/students/{student_id}", response_model=dict)
def get_student(student_id: int, db: Session = Depends(get_db)):
    s = db.get(Student, student_id)
    if not s:
        not_found()
    return {"student": {"id": s.id, "fio": s.fio, "birth_date": s.birth_date, "rating_pos": s.rating_pos, "classroom_id": s.classroom_id}}


@app.post("/api/students", response_model=dict)
def create_student(payload: dict, db: Session = Depends(get_db)):
    if "student" not in payload:
        bad_request("Field 'student' is required")
    try:
        data = StudentIn(**payload["student"])
    except Exception as e:
        bad_request(unwrap_detail(e))

    if not db.get(Classroom, data.classroom_id):
        bad_request("Field 'classroom_id' references missing class")

    try:
        s = Student(
            fio=data.fio,
            birth_date=str(data.birth_date),
            rating_pos=data.rating_pos,
            classroom_id=data.classroom_id,
        )
        db.add(s)
        db.commit()
        db.refresh(s)
        return {"student": {"id": s.id, "fio": s.fio, "birth_date": data.birth_date, "rating_pos": s.rating_pos, "classroom_id": s.classroom_id}}
    except SQLAlchemyError as e:
        db.rollback()
        internal_error(str(e))


@app.patch("/api/students/{student_id}", response_model=dict)
def patch_student(student_id: int, payload: dict, db: Session = Depends(get_db)):
    s = db.get(Student, student_id)
    if not s:
        not_found()
    if "student" not in payload:
        bad_request("Field 'student' is required")

    data = payload["student"]

    if "fio" in data:
        fio = str(data["fio"])
        if not fio.strip():
            bad_request("Field 'fio' is required")
        if len(fio) > 100:
            bad_request("Field 'fio' max length is 100")
        s.fio = fio

    if "birth_date" in data:
        s.birth_date = str(data["birth_date"])

    if "rating_pos" in data:
        rp = int(data["rating_pos"])
        if rp < 1:
            bad_request("Field 'rating_pos' must be >= 1")
        s.rating_pos = rp

    if "classroom_id" in data:
        cid = int(data["classroom_id"])
        if not db.get(Classroom, cid):
            bad_request("Field 'classroom_id' references missing class")
        s.classroom_id = cid

    try:
        db.commit()
        db.refresh(s)
        return {"student": {"id": s.id, "fio": s.fio, "birth_date": s.birth_date, "rating_pos": s.rating_pos, "classroom_id": s.classroom_id}}
    except SQLAlchemyError as e:
        db.rollback()
        internal_error(str(e))


@app.delete("/api/students/{student_id}", status_code=202)
def delete_student(student_id: int, db: Session = Depends(get_db)):
    s = db.get(Student, student_id)
    if not s:
        not_found()
    try:
        db.delete(s)
        db.commit()
        return {"status": 202}
    except SQLAlchemyError as e:
        db.rollback()
        internal_error(str(e))