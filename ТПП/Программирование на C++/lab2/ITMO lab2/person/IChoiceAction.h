// IChoiceAction.h

#ifndef ICHOICEACTION_H
#define ICHOICEACTION_H
#include "IChoiceAction.h"

class IChoiceAction {
public:
    virtual void run() = 0;  // Чисто виртуальный метод
    virtual ~IChoiceAction() = default;
};

#endif // ICHOICEACTION_H
