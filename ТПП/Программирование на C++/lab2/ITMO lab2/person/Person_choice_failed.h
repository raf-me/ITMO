//
// Created by admin on 20.06.2025.
//

#ifndef PERSON_CHOICE_FAILED_H
#define PERSON_CHOICE_FAILED_H
#include "Person.h"
#include "IChoiceAction.h"


class Person_choice_failed : protected Person, public IChoiceAction {
public:
    void run() override;
};



#endif //PERSON_CHOICE_FAILED_H
