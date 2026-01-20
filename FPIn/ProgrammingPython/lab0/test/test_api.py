import pytest
from fastapi.testclient import TestClient
from app.main import app

client = TestClient(app)


def test_teachers_crud():
    r = client.post("/api/teachers", json={"teacher": {"fio": "Ivanov Ivan Ivanovich"}})
    assert r.status_code == 200
    tid = r.json()["teacher"]["id"]

    r = client.get(f"/api/teachers/{tid}")
    assert r.status_code == 200

    r = client.patch(f"/api/teachers/{tid}", json={"teacher": {"fio": "Petrov Petr Petrovich"}})
    assert r.status_code == 200
    assert r.json()["teacher"]["fio"] == "Petrov Petr Petrovich"

    r = client.delete(f"/api/teachers/{tid}")
    assert r.status_code == 202


def test_class_validation_teacher_missing():
    r = client.post("/api/classes", json={"classroom": {"name": "10A", "teacher_id": 9999, "students_count": 10}})
    assert r.status_code == 400


def test_students_flow():
    t = client.post("/api/teachers", json={"teacher": {"fio": "Teacher One"}}).json()["teacher"]["id"]
    c = client.post("/api/classes", json={"classroom": {"name": "11B", "teacher_id": t, "students_count": 0}}).json()[
        "classroom"
    ]["id"]

    r = client.post(
        "/api/students",
        json={"student": {"fio": "Student One", "birth_date": "2008-05-01", "rating_pos": 1, "classroom_id": c}},
    )
    assert r.status_code == 200
    sid = r.json()["student"]["id"]

    r = client.get(f"/api/students/{sid}")
    assert r.status_code == 200

    r = client.delete(f"/api/students/{sid}")
    assert r.status_code == 202