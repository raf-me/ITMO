from fastapi import HTTPException


def bad_request(reason: str):
    raise HTTPException(status_code=400, detail={"status": 400, "reason": reason})


def not_found():
    raise HTTPException(status_code=404, detail={"status": 404, "reason": "Not Found"})


def internal_error(reason: str):
    raise HTTPException(status_code=500, detail={"status": 500, "reason": reason})