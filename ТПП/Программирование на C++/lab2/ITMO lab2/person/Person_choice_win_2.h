//
// Created by admin on 20.06.2025.
//

#ifndef PERSON_CHOICE_WIN_2_H
#define PERSON_CHOICE_WIN_2_H
#include "IChoiceAction.h"
#include "Person.h"


class Person_choice_win_2 : protected Person, public IChoiceAction {
public:
    void run() override;
};



#endif //PERSON_CHOICE_WIN_2_H
