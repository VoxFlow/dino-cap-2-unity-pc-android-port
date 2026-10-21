# C++ classes and virtual tables

> *AI-reviewed content: may change at any time and should not be fully trusted. See the [disclaimer](../../start/disclaimer.md).*

116 virtual tables (`__ZTV` symbols). Slots follow the Itanium C++ ABI used by the iOS toolchain: after the offset-to-top and RTTI words, one function pointer per slot (Thumb pointers have the low bit set; the target column shows the code address). **File word** is the 32-bit value in the executable file; where the loader writes the slot through a relocation the file holds 0 and the **resolved word** is the value IDA shows after binding (165 of 851 slots). The two are kept apart: a resolved word is not a byte of the file. Headings give the class name; `classes.jsonl` keeps the demangler's raw text, which wraps it as `` `vtable for'X `` (the backquote and apostrophe are the demangler's quoting).

## DinoCap::CAObjCallBack

Virtual table at `0x22C680`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0xA1A3` | - | `0xA1A3` | `CAObjCallBack::~CAObjCallBack` |
| 1 | `0x4` | `0xA2C3` | - | `0xA2C3` | `CAObjCallBack::~CAObjCallBack` |
| 2 | `0x8` | `0x183D29` | - | `0x183D29` | `Triniti2D::CppGameObjectCallBack::Attach` |
| 3 | `0xC` | `0x183D2D` | - | `0x183D2D` | `Triniti2D::CppGameObjectCallBack::Detach` |
| 4 | `0x10` | `0x183D33` | - | `0x183D33` | `Triniti2D::CppGameObjectCallBack::OnLoad` |
| 5 | `0x14` | `0x183D35` | - | `0x183D35` | `Triniti2D::CppGameObjectCallBack::OnUnload` |
| 6 | `0x18` | `0xA6CD` | - | `0xA6CD` | [`CAObjCallBack::OnUpdate`](../functions/dossiers/CAObjCallBack/FUN-00A6CC.md) |
| 7 | `0x1C` | `0x183D39` | - | `0x183D39` | `Triniti2D::CppGameObjectCallBack::OnTimeout` |
| 8 | `0x20` | `0x183D3B` | - | `0x183D3B` | `Triniti2D::CppGameObjectCallBack::OnGameEvent` |
| 9 | `0x24` | `0xA011` | - | `0xA011` | `CAObjCallBack::OnAnimationEnd` |
| 10 | `0x28` | `0xA013` | - | `0xA013` | `CAObjCallBack::OnAnimationEndAdv` |
| 11 | `0x2C` | `0xA015` | - | `0xA015` | `CAObjCallBack::OnKeyFrame` |
| 12 | `0x30` | `0xA017` | - | `0xA017` | `CAObjCallBack::OnKeyFrameAdv` |

## DinoCap::CAObjSpriteCallBack

Virtual table at `0x22C6D4`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0x0` | loader binds `__ZN7DinoCap19CAObjSpriteCallBackD1Ev` | `0xB21E` | `CAObjSpriteCallBack::~CAObjSpriteCallBack` |
| 1 | `0x4` | `0x0` | loader binds `__ZN7DinoCap19CAObjSpriteCallBackD0Ev` | `0xB25C` | `CAObjSpriteCallBack::~CAObjSpriteCallBack` |
| 2 | `0x8` | `0x183D29` | - | `0x183D29` | `Triniti2D::CppGameObjectCallBack::Attach` |
| 3 | `0xC` | `0x183D2D` | - | `0x183D2D` | `Triniti2D::CppGameObjectCallBack::Detach` |
| 4 | `0x10` | `0x183D33` | - | `0x183D33` | `Triniti2D::CppGameObjectCallBack::OnLoad` |
| 5 | `0x14` | `0x183D35` | - | `0x183D35` | `Triniti2D::CppGameObjectCallBack::OnUnload` |
| 6 | `0x18` | `0x183D37` | - | `0x183D37` | `Triniti2D::CppGameObjectCallBack::OnUpdate` |
| 7 | `0x1C` | `0x183D39` | - | `0x183D39` | `Triniti2D::CppGameObjectCallBack::OnTimeout` |
| 8 | `0x20` | `0x183D3B` | - | `0x183D3B` | `Triniti2D::CppGameObjectCallBack::OnGameEvent` |

## DinoCap::CompAnimObj

Virtual table at `0x22D100`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0x119545` | - | `0x119545` | `CompAnimObj::~CompAnimObj` |
| 1 | `0x4` | `0x1199D1` | - | `0x1199D1` | `CompAnimObj::~CompAnimObj` |
| 2 | `0x8` | `0x183D29` | - | `0x183D29` | `Triniti2D::CppGameObjectCallBack::Attach` |
| 3 | `0xC` | `0x183D2D` | - | `0x183D2D` | `Triniti2D::CppGameObjectCallBack::Detach` |
| 4 | `0x10` | `0x11790D` | - | `0x11790D` | `CompAnimObj::OnLoad` |
| 5 | `0x14` | `0x183D35` | - | `0x183D35` | `Triniti2D::CppGameObjectCallBack::OnUnload` |
| 6 | `0x18` | `0x1190D5` | - | `0x1190D5` | [`CompAnimObj::OnUpdate`](../functions/dossiers/CompAnimObj/FUN-1190D4.md) |
| 7 | `0x1C` | `0x183D39` | - | `0x183D39` | `Triniti2D::CppGameObjectCallBack::OnTimeout` |
| 8 | `0x20` | `0x183D3B` | - | `0x183D3B` | `Triniti2D::CppGameObjectCallBack::OnGameEvent` |
| 9 | `0x24` | `0x1179DD` | - | `0x1179DD` | `CompAnimObj::OnDraw` |
| 10 | `0x28` | `0x11790F` | - | `0x11790F` | `CompAnimObj::OnAnimationEnd` |
| 11 | `0x2C` | `0x117911` | - | `0x117911` | `CompAnimObj::OnAnimationEndAdv` |
| 12 | `0x30` | `0x117913` | - | `0x117913` | `CompAnimObj::OnKeyFrame` |
| 13 | `0x34` | `0x117915` | - | `0x117915` | `CompAnimObj::OnKeyFrameAdv` |

## DinoCap::CompAnimObjSprite

Virtual table at `0x22D174`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0x0` | loader binds `__ZN7DinoCap17CompAnimObjSpriteD1Ev` | `0x11C0F8` | `CompAnimObjSprite::~CompAnimObjSprite` |
| 1 | `0x4` | `0x0` | loader binds `__ZN7DinoCap17CompAnimObjSpriteD0Ev` | `0x11B574` | `CompAnimObjSprite::~CompAnimObjSprite` |
| 2 | `0x8` | `0x17747D` | - | `0x17747D` | `Triniti2D::Sprite::GetPointer` |

## DinoCap::TiXmlAttribute

Virtual table at `0x22CB68`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0x0` | loader binds `__ZN7DinoCap14TiXmlAttributeD1Ev` | `0xC54A0` | `TiXmlAttribute::~TiXmlAttribute` |
| 1 | `0x4` | `0x0` | loader binds `__ZN7DinoCap14TiXmlAttributeD0Ev` | `0xC55A2` | `TiXmlAttribute::~TiXmlAttribute` |
| 2 | `0x8` | `0x0` | loader binds `__ZNK7DinoCap14TiXmlAttribute5PrintEP7__sFILEi` | `0xC541C` | `TiXmlAttribute::Print` |
| 3 | `0xC` | `0xC4631` | - | `0xC4631` | `TiXmlAttribute::Parse` |

## DinoCap::TiXmlBase

Virtual table at `0x22CB50`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0x0` | loader binds `__ZN7DinoCap9TiXmlBaseD1Ev` | `0xC355C` | `TiXmlBase::~TiXmlBase` |
| 1 | `0x4` | `0x0` | loader binds `__ZN7DinoCap9TiXmlBaseD0Ev` | `0xC5404` | `TiXmlBase::~TiXmlBase` |

## DinoCap::TiXmlComment

Virtual table at `0x22CA10`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0x0` | loader binds `__ZN7DinoCap12TiXmlCommentD1Ev` | `0xC3798` | `TiXmlComment::~TiXmlComment` |
| 1 | `0x4` | `0x0` | loader binds `__ZN7DinoCap12TiXmlCommentD0Ev` | `0xC3778` | `TiXmlComment::~TiXmlComment` |
| 2 | `0x8` | `0xC22C1` | - | `0xC22C1` | `TiXmlComment::Print` |
| 3 | `0xC` | `0xC4099` | - | `0xC4099` | `TiXmlComment::Parse` |
| 4 | `0x10` | `0x0` | loader binds `__ZNK7DinoCap9TiXmlNode10ToDocumentEv` | `0xC3508` | `TiXmlNode::ToDocument` |
| 5 | `0x14` | `0x0` | loader binds `__ZNK7DinoCap9TiXmlNode9ToElementEv` | `0xC350C` | `TiXmlNode::ToElement` |
| 6 | `0x18` | `0x0` | loader binds `__ZNK7DinoCap12TiXmlComment9ToCommentEv` | `0xC353C` | `TiXmlComment::ToComment` |
| 7 | `0x1C` | `0x0` | loader binds `__ZNK7DinoCap9TiXmlNode9ToUnknownEv` | `0xC3514` | `TiXmlNode::ToUnknown` |
| 8 | `0x20` | `0x0` | loader binds `__ZNK7DinoCap9TiXmlNode6ToTextEv` | `0xC3518` | `TiXmlNode::ToText` |
| 9 | `0x24` | `0x0` | loader binds `__ZNK7DinoCap9TiXmlNode13ToDeclarationEv` | `0xC351C` | `TiXmlNode::ToDeclaration` |
| 10 | `0x28` | `0x0` | loader binds `__ZN7DinoCap9TiXmlNode10ToDocumentEv` | `0xC3520` | `TiXmlNode::ToDocument` |
| 11 | `0x2C` | `0x0` | loader binds `__ZN7DinoCap9TiXmlNode9ToElementEv` | `0xC3524` | `TiXmlNode::ToElement` |
| 12 | `0x30` | `0x0` | loader binds `__ZN7DinoCap12TiXmlComment9ToCommentEv` | `0xC353E` | `TiXmlComment::ToComment` |
| 13 | `0x34` | `0x0` | loader binds `__ZN7DinoCap9TiXmlNode9ToUnknownEv` | `0xC352C` | `TiXmlNode::ToUnknown` |
| 14 | `0x38` | `0x0` | loader binds `__ZN7DinoCap9TiXmlNode6ToTextEv` | `0xC3530` | `TiXmlNode::ToText` |
| 15 | `0x3C` | `0x0` | loader binds `__ZN7DinoCap9TiXmlNode13ToDeclarationEv` | `0xC3534` | `TiXmlNode::ToDeclaration` |
| 16 | `0x40` | `0xC2685` | - | `0xC2685` | `TiXmlComment::Clone` |
| 17 | `0x44` | `0xC1E13` | - | `0xC1E13` | `TiXmlComment::Accept` |

## DinoCap::TiXmlDeclaration

Virtual table at `0x22C96C`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0x0` | loader binds `__ZN7DinoCap16TiXmlDeclarationD1Ev` | `0xC3816` | `TiXmlDeclaration::~TiXmlDeclaration` |
| 1 | `0x4` | `0x0` | loader binds `__ZN7DinoCap16TiXmlDeclarationD0Ev` | `0xC394C` | `TiXmlDeclaration::~TiXmlDeclaration` |
| 2 | `0x8` | `0x0` | loader binds `__ZNK7DinoCap16TiXmlDeclaration5PrintEP7__sFILEi` | `0xC3544` | `TiXmlDeclaration::Print` |
| 3 | `0xC` | `0xC477D` | - | `0xC477D` | `TiXmlDeclaration::Parse` |
| 4 | `0x10` | `0x0` | loader binds `__ZNK7DinoCap9TiXmlNode10ToDocumentEv` | `0xC3508` | `TiXmlNode::ToDocument` |
| 5 | `0x14` | `0x0` | loader binds `__ZNK7DinoCap9TiXmlNode9ToElementEv` | `0xC350C` | `TiXmlNode::ToElement` |
| 6 | `0x18` | `0x0` | loader binds `__ZNK7DinoCap9TiXmlNode9ToCommentEv` | `0xC3510` | `TiXmlNode::ToComment` |
| 7 | `0x1C` | `0x0` | loader binds `__ZNK7DinoCap9TiXmlNode9ToUnknownEv` | `0xC3514` | `TiXmlNode::ToUnknown` |
| 8 | `0x20` | `0x0` | loader binds `__ZNK7DinoCap9TiXmlNode6ToTextEv` | `0xC3518` | `TiXmlNode::ToText` |
| 9 | `0x24` | `0x0` | loader binds `__ZNK7DinoCap16TiXmlDeclaration13ToDeclarationEv` | `0xC3552` | `TiXmlDeclaration::ToDeclaration` |
| 10 | `0x28` | `0x0` | loader binds `__ZN7DinoCap9TiXmlNode10ToDocumentEv` | `0xC3520` | `TiXmlNode::ToDocument` |
| 11 | `0x2C` | `0x0` | loader binds `__ZN7DinoCap9TiXmlNode9ToElementEv` | `0xC3524` | `TiXmlNode::ToElement` |
| 12 | `0x30` | `0x0` | loader binds `__ZN7DinoCap9TiXmlNode9ToCommentEv` | `0xC3528` | `TiXmlNode::ToComment` |
| 13 | `0x34` | `0x0` | loader binds `__ZN7DinoCap9TiXmlNode9ToUnknownEv` | `0xC352C` | `TiXmlNode::ToUnknown` |
| 14 | `0x38` | `0x0` | loader binds `__ZN7DinoCap9TiXmlNode6ToTextEv` | `0xC3530` | `TiXmlNode::ToText` |
| 15 | `0x3C` | `0x0` | loader binds `__ZN7DinoCap16TiXmlDeclaration13ToDeclarationEv` | `0xC3554` | `TiXmlDeclaration::ToDeclaration` |
| 16 | `0x40` | `0xC2629` | - | `0xC2629` | `TiXmlDeclaration::Clone` |
| 17 | `0x44` | `0xC1E37` | - | `0xC1E37` | `TiXmlDeclaration::Accept` |
| 18 | `0x48` | `0xC3159` | - | `0xC3159` | `TiXmlDeclaration::Print` |

## DinoCap::TiXmlDocument

Virtual table at `0x22CB80`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0x0` | loader binds `__ZN7DinoCap13TiXmlDocumentD1Ev` | `0xC5556` | `TiXmlDocument::~TiXmlDocument` |
| 1 | `0x4` | `0x0` | loader binds `__ZN7DinoCap13TiXmlDocumentD0Ev` | `0xC5568` | `TiXmlDocument::~TiXmlDocument` |
| 2 | `0x8` | `0xC24D9` | - | `0xC24D9` | `TiXmlDocument::Print` |
| 3 | `0xC` | `0xC5255` | - | `0xC5255` | `TiXmlDocument::Parse` |
| 4 | `0x10` | `0x0` | loader binds `__ZNK7DinoCap13TiXmlDocument10ToDocumentEv` | `0xC5400` | `TiXmlDocument::ToDocument` |
| 5 | `0x14` | `0x0` | loader binds `__ZNK7DinoCap9TiXmlNode9ToElementEv` | `0xC350C` | `TiXmlNode::ToElement` |
| 6 | `0x18` | `0x0` | loader binds `__ZNK7DinoCap9TiXmlNode9ToCommentEv` | `0xC3510` | `TiXmlNode::ToComment` |
| 7 | `0x1C` | `0x0` | loader binds `__ZNK7DinoCap9TiXmlNode9ToUnknownEv` | `0xC3514` | `TiXmlNode::ToUnknown` |
| 8 | `0x20` | `0x0` | loader binds `__ZNK7DinoCap9TiXmlNode6ToTextEv` | `0xC3518` | `TiXmlNode::ToText` |
| 9 | `0x24` | `0x0` | loader binds `__ZNK7DinoCap9TiXmlNode13ToDeclarationEv` | `0xC351C` | `TiXmlNode::ToDeclaration` |
| 10 | `0x28` | `0x0` | loader binds `__ZN7DinoCap13TiXmlDocument10ToDocumentEv` | `0xC5402` | `TiXmlDocument::ToDocument` |
| 11 | `0x2C` | `0x0` | loader binds `__ZN7DinoCap9TiXmlNode9ToElementEv` | `0xC3524` | `TiXmlNode::ToElement` |
| 12 | `0x30` | `0x0` | loader binds `__ZN7DinoCap9TiXmlNode9ToCommentEv` | `0xC3528` | `TiXmlNode::ToComment` |
| 13 | `0x34` | `0x0` | loader binds `__ZN7DinoCap9TiXmlNode9ToUnknownEv` | `0xC352C` | `TiXmlNode::ToUnknown` |
| 14 | `0x38` | `0x0` | loader binds `__ZN7DinoCap9TiXmlNode6ToTextEv` | `0xC3530` | `TiXmlNode::ToText` |
| 15 | `0x3C` | `0x0` | loader binds `__ZN7DinoCap9TiXmlNode13ToDeclarationEv` | `0xC3534` | `TiXmlNode::ToDeclaration` |
| 16 | `0x40` | `0xC29AB` | - | `0xC29AB` | `TiXmlDocument::Clone` |
| 17 | `0x44` | `0xC249F` | - | `0xC249F` | `TiXmlDocument::Accept` |

## DinoCap::TiXmlElement

Virtual table at `0x22CA60`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0xC2BDB` | - | `0xC2BDB` | `TiXmlElement::~TiXmlElement` |
| 1 | `0x4` | `0xC2A55` | - | `0xC2A55` | `TiXmlElement::~TiXmlElement` |
| 2 | `0x8` | `0xC232D` | - | `0xC232D` | `TiXmlElement::Print` |
| 3 | `0xC` | `0xC4EC9` | - | `0xC4EC9` | `TiXmlElement::Parse` |
| 4 | `0x10` | `0x0` | loader binds `__ZNK7DinoCap9TiXmlNode10ToDocumentEv` | `0xC3508` | `TiXmlNode::ToDocument` |
| 5 | `0x14` | `0x0` | loader binds `__ZNK7DinoCap12TiXmlElement9ToElementEv` | `0xC3538` | `TiXmlElement::ToElement` |
| 6 | `0x18` | `0x0` | loader binds `__ZNK7DinoCap9TiXmlNode9ToCommentEv` | `0xC3510` | `TiXmlNode::ToComment` |
| 7 | `0x1C` | `0x0` | loader binds `__ZNK7DinoCap9TiXmlNode9ToUnknownEv` | `0xC3514` | `TiXmlNode::ToUnknown` |
| 8 | `0x20` | `0x0` | loader binds `__ZNK7DinoCap9TiXmlNode6ToTextEv` | `0xC3518` | `TiXmlNode::ToText` |
| 9 | `0x24` | `0x0` | loader binds `__ZNK7DinoCap9TiXmlNode13ToDeclarationEv` | `0xC351C` | `TiXmlNode::ToDeclaration` |
| 10 | `0x28` | `0x0` | loader binds `__ZN7DinoCap9TiXmlNode10ToDocumentEv` | `0xC3520` | `TiXmlNode::ToDocument` |
| 11 | `0x2C` | `0x0` | loader binds `__ZN7DinoCap12TiXmlElement9ToElementEv` | `0xC353A` | `TiXmlElement::ToElement` |
| 12 | `0x30` | `0x0` | loader binds `__ZN7DinoCap9TiXmlNode9ToCommentEv` | `0xC3528` | `TiXmlNode::ToComment` |
| 13 | `0x34` | `0x0` | loader binds `__ZN7DinoCap9TiXmlNode9ToUnknownEv` | `0xC352C` | `TiXmlNode::ToUnknown` |
| 14 | `0x38` | `0x0` | loader binds `__ZN7DinoCap9TiXmlNode6ToTextEv` | `0xC3530` | `TiXmlNode::ToText` |
| 15 | `0x3C` | `0x0` | loader binds `__ZN7DinoCap9TiXmlNode13ToDeclarationEv` | `0xC3534` | `TiXmlNode::ToDeclaration` |
| 16 | `0x40` | `0xC2EC1` | - | `0xC2EC1` | `TiXmlElement::Clone` |
| 17 | `0x44` | `0xC252B` | - | `0xC252B` | `TiXmlElement::Accept` |

## DinoCap::TiXmlNode

Virtual table at `0x22CAB0`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0xC2CAF` | - | `0xC2CAF` | `TiXmlNode::~TiXmlNode` |
| 1 | `0x4` | `0xC33F9` | - | `0xC33F9` | `TiXmlNode::~TiXmlNode` |

## DinoCap::TiXmlText

Virtual table at `0x22C9C0`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0x0` | loader binds `__ZN7DinoCap9TiXmlTextD1Ev` | `0xC36B4` | `TiXmlText::~TiXmlText` |
| 1 | `0x4` | `0x0` | loader binds `__ZN7DinoCap9TiXmlTextD0Ev` | `0xC3694` | `TiXmlText::~TiXmlText` |
| 2 | `0x8` | `0xC32A5` | - | `0xC32A5` | `TiXmlText::Print` |
| 3 | `0xC` | `0xC438D` | - | `0xC438D` | `TiXmlText::Parse` |
| 4 | `0x10` | `0x0` | loader binds `__ZNK7DinoCap9TiXmlNode10ToDocumentEv` | `0xC3508` | `TiXmlNode::ToDocument` |
| 5 | `0x14` | `0x0` | loader binds `__ZNK7DinoCap9TiXmlNode9ToElementEv` | `0xC350C` | `TiXmlNode::ToElement` |
| 6 | `0x18` | `0x0` | loader binds `__ZNK7DinoCap9TiXmlNode9ToCommentEv` | `0xC3510` | `TiXmlNode::ToComment` |
| 7 | `0x1C` | `0x0` | loader binds `__ZNK7DinoCap9TiXmlNode9ToUnknownEv` | `0xC3514` | `TiXmlNode::ToUnknown` |
| 8 | `0x20` | `0x0` | loader binds `__ZNK7DinoCap9TiXmlText6ToTextEv` | `0xC3540` | `TiXmlText::ToText` |
| 9 | `0x24` | `0x0` | loader binds `__ZNK7DinoCap9TiXmlNode13ToDeclarationEv` | `0xC351C` | `TiXmlNode::ToDeclaration` |
| 10 | `0x28` | `0x0` | loader binds `__ZN7DinoCap9TiXmlNode10ToDocumentEv` | `0xC3520` | `TiXmlNode::ToDocument` |
| 11 | `0x2C` | `0x0` | loader binds `__ZN7DinoCap9TiXmlNode9ToElementEv` | `0xC3524` | `TiXmlNode::ToElement` |
| 12 | `0x30` | `0x0` | loader binds `__ZN7DinoCap9TiXmlNode9ToCommentEv` | `0xC3528` | `TiXmlNode::ToComment` |
| 13 | `0x34` | `0x0` | loader binds `__ZN7DinoCap9TiXmlNode9ToUnknownEv` | `0xC352C` | `TiXmlNode::ToUnknown` |
| 14 | `0x38` | `0x0` | loader binds `__ZN7DinoCap9TiXmlText6ToTextEv` | `0xC3542` | `TiXmlText::ToText` |
| 15 | `0x3C` | `0x0` | loader binds `__ZN7DinoCap9TiXmlNode13ToDeclarationEv` | `0xC3534` | `TiXmlNode::ToDeclaration` |
| 16 | `0x40` | `0xC2CB9` | - | `0xC2CB9` | `TiXmlText::Clone` |
| 17 | `0x44` | `0xC1E25` | - | `0xC1E25` | `TiXmlText::Accept` |

## DinoCap::TiXmlUnknown

Virtual table at `0x22C91C`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0x0` | loader binds `__ZN7DinoCap12TiXmlUnknownD1Ev` | `0xC367C` | `TiXmlUnknown::~TiXmlUnknown` |
| 1 | `0x4` | `0x0` | loader binds `__ZN7DinoCap12TiXmlUnknownD0Ev` | `0xC365C` | `TiXmlUnknown::~TiXmlUnknown` |
| 2 | `0x8` | `0xC227B` | - | `0xC227B` | `TiXmlUnknown::Print` |
| 3 | `0xC` | `0xC4169` | - | `0xC4169` | `TiXmlUnknown::Parse` |
| 4 | `0x10` | `0x0` | loader binds `__ZNK7DinoCap9TiXmlNode10ToDocumentEv` | `0xC3508` | `TiXmlNode::ToDocument` |
| 5 | `0x14` | `0x0` | loader binds `__ZNK7DinoCap9TiXmlNode9ToElementEv` | `0xC350C` | `TiXmlNode::ToElement` |
| 6 | `0x18` | `0x0` | loader binds `__ZNK7DinoCap9TiXmlNode9ToCommentEv` | `0xC3510` | `TiXmlNode::ToComment` |
| 7 | `0x1C` | `0x0` | loader binds `__ZNK7DinoCap12TiXmlUnknown9ToUnknownEv` | `0xC3556` | `TiXmlUnknown::ToUnknown` |
| 8 | `0x20` | `0x0` | loader binds `__ZNK7DinoCap9TiXmlNode6ToTextEv` | `0xC3518` | `TiXmlNode::ToText` |
| 9 | `0x24` | `0x0` | loader binds `__ZNK7DinoCap9TiXmlNode13ToDeclarationEv` | `0xC351C` | `TiXmlNode::ToDeclaration` |
| 10 | `0x28` | `0x0` | loader binds `__ZN7DinoCap9TiXmlNode10ToDocumentEv` | `0xC3520` | `TiXmlNode::ToDocument` |
| 11 | `0x2C` | `0x0` | loader binds `__ZN7DinoCap9TiXmlNode9ToElementEv` | `0xC3524` | `TiXmlNode::ToElement` |
| 12 | `0x30` | `0x0` | loader binds `__ZN7DinoCap9TiXmlNode9ToCommentEv` | `0xC3528` | `TiXmlNode::ToComment` |
| 13 | `0x34` | `0x0` | loader binds `__ZN7DinoCap12TiXmlUnknown9ToUnknownEv` | `0xC3558` | `TiXmlUnknown::ToUnknown` |
| 14 | `0x38` | `0x0` | loader binds `__ZN7DinoCap9TiXmlNode6ToTextEv` | `0xC3530` | `TiXmlNode::ToText` |
| 15 | `0x3C` | `0x0` | loader binds `__ZN7DinoCap9TiXmlNode13ToDeclarationEv` | `0xC3534` | `TiXmlNode::ToDeclaration` |
| 16 | `0x40` | `0xC25BD` | - | `0xC25BD` | `TiXmlUnknown::Clone` |
| 17 | `0x44` | `0xC1E49` | - | `0xC1E49` | `TiXmlUnknown::Accept` |

## DinoCap::UILabelMoveCtrl

Virtual table at `0x22CCE0`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0xCD539` | - | `0xCD539` | `UILabelMoveCtrl::~UILabelMoveCtrl` |
| 1 | `0x4` | `0xCD503` | - | `0xCD503` | `UILabelMoveCtrl::~UILabelMoveCtrl` |
| 2 | `0x8` | `0x17E657` | - | `0x17E657` | `Triniti2D::UIControl::Initialize` |
| 3 | `0xC` | `0x17FD7F` | - | `0x17FD7F` | `Triniti2D::UILabel::SetRect` |
| 4 | `0x10` | `0x17E673` | - | `0x17E673` | `Triniti2D::UIControl::GetRect` |
| 5 | `0x14` | `0x17E683` | - | `0x17E683` | `Triniti2D::UIControl::SetRectPosition` |
| 6 | `0x18` | `0x17F063` | - | `0x17F063` | `Triniti2D::UIControl::GetRectPosition` |
| 7 | `0x1C` | `0x17E6AD` | - | `0x17E6AD` | `Triniti2D::UIControl::SetRectSize` |
| 8 | `0x20` | `0x17F045` | - | `0x17F045` | `Triniti2D::UIControl::GetRectSize` |
| 9 | `0x24` | `0x17E6D7` | - | `0x17E6D7` | `Triniti2D::UIControl::SetVisible` |
| 10 | `0x28` | `0x17E6DD` | - | `0x17E6DD` | `Triniti2D::UIControl::GetVisible` |
| 11 | `0x2C` | `0x17E6E3` | - | `0x17E6E3` | `Triniti2D::UIControl::SetEnable` |
| 12 | `0x30` | `0x17E6E9` | - | `0x17E6E9` | `Triniti2D::UIControl::GetEnable` |
| 13 | `0x34` | `0x17F249` | - | `0x17F249` | `Triniti2D::UIControlVisible::SetClip` |
| 14 | `0x38` | `0x17F209` | - | `0x17F209` | `Triniti2D::UIControlVisible::ClearClip` |
| 15 | `0x3C` | `0x17E70D` | - | `0x17E70D` | `Triniti2D::UIControl::PtInRect` |
| 16 | `0x40` | `0x17F081` | - | `0x17F081` | `Triniti2D::UIControl::FireUIEvent` |
| 17 | `0x44` | `0x17FF39` | - | `0x17FF39` | `Triniti2D::UILabel::Draw` |
| 18 | `0x48` | `0xCDA99` | - | `0xCDA99` | [`UILabelMoveCtrl::Update`](../functions/dossiers/UILabelMoveCtrl/FUN-0CDA98.md) |
| 19 | `0x4C` | `0x17FD99` | - | `0x17FD99` | `Triniti2D::UILabel::HandleTouch` |
| 20 | `0x50` | `0x180041` | - | `0x180041` | `Triniti2D::UILabel::UpdateText` |

## DinoCap::UIMoveCtrl

Virtual table at `0x22CD3C`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0xCD699` | - | `0xCD699` | `UIMoveCtrl::~UIMoveCtrl` |
| 1 | `0x4` | `0xCD661` | - | `0xCD661` | `UIMoveCtrl::~UIMoveCtrl` |
| 2 | `0x8` | `0x17F5E1` | - | `0x17F5E1` | `Triniti2D::UIImage::Initialize` |
| 3 | `0xC` | `0x17F18F` | - | `0x17F18F` | `Triniti2D::UIImage::SetRect` |
| 4 | `0x10` | `0x17E673` | - | `0x17E673` | `Triniti2D::UIControl::GetRect` |
| 5 | `0x14` | `0x17E683` | - | `0x17E683` | `Triniti2D::UIControl::SetRectPosition` |
| 6 | `0x18` | `0x17F063` | - | `0x17F063` | `Triniti2D::UIControl::GetRectPosition` |
| 7 | `0x1C` | `0x17E6AD` | - | `0x17E6AD` | `Triniti2D::UIControl::SetRectSize` |
| 8 | `0x20` | `0x17F045` | - | `0x17F045` | `Triniti2D::UIControl::GetRectSize` |
| 9 | `0x24` | `0x17E6D7` | - | `0x17E6D7` | `Triniti2D::UIControl::SetVisible` |
| 10 | `0x28` | `0x17E6DD` | - | `0x17E6DD` | `Triniti2D::UIControl::GetVisible` |
| 11 | `0x2C` | `0x17E6E3` | - | `0x17E6E3` | `Triniti2D::UIControl::SetEnable` |
| 12 | `0x30` | `0x17E6E9` | - | `0x17E6E9` | `Triniti2D::UIControl::GetEnable` |
| 13 | `0x34` | `0x17F249` | - | `0x17F249` | `Triniti2D::UIControlVisible::SetClip` |
| 14 | `0x38` | `0x17F209` | - | `0x17F209` | `Triniti2D::UIControlVisible::ClearClip` |
| 15 | `0x3C` | `0x17E70D` | - | `0x17E70D` | `Triniti2D::UIControl::PtInRect` |
| 16 | `0x40` | `0x17F081` | - | `0x17F081` | `Triniti2D::UIControl::FireUIEvent` |
| 17 | `0x44` | `0x17F291` | - | `0x17F291` | `Triniti2D::UIImage::Draw` |
| 18 | `0x48` | `0xCD797` | - | `0xCD797` | [`UIMoveCtrl::Update`](../functions/dossiers/UIMoveCtrl/FUN-0CD796.md) |
| 19 | `0x4C` | `0xCD543` | - | `0xCD543` | [`UIMoveCtrl::HandleTouch`](../functions/dossiers/UIMoveCtrl/FUN-0CD542.md) |

## DinoCap::baozhaClass

Virtual table at `0x22C784`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0x15FE9` | - | `0x15FE9` | `baozhaClass::~baozhaClass` |
| 1 | `0x4` | `0x15FB3` | - | `0x15FB3` | `baozhaClass::~baozhaClass` |
| 2 | `0x8` | `0x183D29` | - | `0x183D29` | `Triniti2D::CppGameObjectCallBack::Attach` |
| 3 | `0xC` | `0x183D2D` | - | `0x183D2D` | `Triniti2D::CppGameObjectCallBack::Detach` |
| 4 | `0x10` | `0x183D33` | - | `0x183D33` | `Triniti2D::CppGameObjectCallBack::OnLoad` |
| 5 | `0x14` | `0x183D35` | - | `0x183D35` | `Triniti2D::CppGameObjectCallBack::OnUnload` |
| 6 | `0x18` | `0xA6CD` | - | `0xA6CD` | [`CAObjCallBack::OnUpdate`](../functions/dossiers/CAObjCallBack/FUN-00A6CC.md) |
| 7 | `0x1C` | `0x183D39` | - | `0x183D39` | `Triniti2D::CppGameObjectCallBack::OnTimeout` |
| 8 | `0x20` | `0x183D3B` | - | `0x183D3B` | `Triniti2D::CppGameObjectCallBack::OnGameEvent` |
| 9 | `0x24` | `0x15EE1` | - | `0x15EE1` | `baozhaClass::OnAnimationEnd` |
| 10 | `0x28` | `0xA013` | - | `0xA013` | `CAObjCallBack::OnAnimationEndAdv` |
| 11 | `0x2C` | `0xA015` | - | `0xA015` | `CAObjCallBack::OnKeyFrame` |
| 12 | `0x30` | `0xA017` | - | `0xA017` | `CAObjCallBack::OnKeyFrameAdv` |

## DinoCap::bglayerClass

Virtual table at `0x22C548`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0x6DE7` | - | `0x6DE7` | `bglayerClass::~bglayerClass` |
| 1 | `0x4` | `0x6D8B` | - | `0x6D8B` | `bglayerClass::~bglayerClass` |
| 2 | `0x8` | `0x183D29` | - | `0x183D29` | `Triniti2D::CppGameObjectCallBack::Attach` |
| 3 | `0xC` | `0x183D2D` | - | `0x183D2D` | `Triniti2D::CppGameObjectCallBack::Detach` |
| 4 | `0x10` | `0x183D33` | - | `0x183D33` | `Triniti2D::CppGameObjectCallBack::OnLoad` |
| 5 | `0x14` | `0x183D35` | - | `0x183D35` | `Triniti2D::CppGameObjectCallBack::OnUnload` |
| 6 | `0x18` | `0x700D` | - | `0x700D` | [`bglayerClass::OnUpdate`](../functions/dossiers/bglayerClass/FUN-00700C.md) |
| 7 | `0x1C` | `0x183D39` | - | `0x183D39` | `Triniti2D::CppGameObjectCallBack::OnTimeout` |
| 8 | `0x20` | `0x183D3B` | - | `0x183D3B` | `Triniti2D::CppGameObjectCallBack::OnGameEvent` |

## DinoCap::bossN

Virtual table at `0x22D23C`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0x120D59` | - | `0x120D59` | `bossN::~bossN` |
| 1 | `0x4` | `0x12100D` | - | `0x12100D` | `bossN::~bossN` |
| 2 | `0x8` | `0x183D29` | - | `0x183D29` | `Triniti2D::CppGameObjectCallBack::Attach` |
| 3 | `0xC` | `0x183D2D` | - | `0x183D2D` | `Triniti2D::CppGameObjectCallBack::Detach` |
| 4 | `0x10` | `0x1210ED` | - | `0x1210ED` | [`bossN::OnLoad`](../functions/dossiers/bossN/FUN-1210EC.md) |
| 5 | `0x14` | `0x183D35` | - | `0x183D35` | `Triniti2D::CppGameObjectCallBack::OnUnload` |
| 6 | `0x18` | `0x121A99` | - | `0x121A99` | [`bossN::OnUpdate`](../functions/dossiers/bossN/FUN-121A98.md) |
| 7 | `0x1C` | `0x1217A9` | - | `0x1217A9` | [`bossN::OnTimeout`](../functions/dossiers/bossN/FUN-1217A8.md) |
| 8 | `0x20` | `0x183D3B` | - | `0x183D3B` | `Triniti2D::CppGameObjectCallBack::OnGameEvent` |
| 9 | `0x24` | `0x1179DD` | - | `0x1179DD` | `CompAnimObj::OnDraw` |
| 10 | `0x28` | `0x11790F` | - | `0x11790F` | `CompAnimObj::OnAnimationEnd` |
| 11 | `0x2C` | `0x117911` | - | `0x117911` | `CompAnimObj::OnAnimationEndAdv` |
| 12 | `0x30` | `0x117913` | - | `0x117913` | `CompAnimObj::OnKeyFrame` |
| 13 | `0x34` | `0x117915` | - | `0x117915` | `CompAnimObj::OnKeyFrameAdv` |

## DinoCap::bulletClass

Virtual table at `0x22C630`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0x74B9` | - | `0x74B9` | `bulletClass::~bulletClass` |
| 1 | `0x4` | `0x7483` | - | `0x7483` | `bulletClass::~bulletClass` |
| 2 | `0x8` | `0x183D29` | - | `0x183D29` | `Triniti2D::CppGameObjectCallBack::Attach` |
| 3 | `0xC` | `0x183D2D` | - | `0x183D2D` | `Triniti2D::CppGameObjectCallBack::Detach` |
| 4 | `0x10` | `0x183D33` | - | `0x183D33` | `Triniti2D::CppGameObjectCallBack::OnLoad` |
| 5 | `0x14` | `0x183D35` | - | `0x183D35` | `Triniti2D::CppGameObjectCallBack::OnUnload` |
| 6 | `0x18` | `0x77A5` | - | `0x77A5` | [`bulletClass::OnUpdate`](../functions/dossiers/bulletClass/FUN-0077A4.md) |
| 7 | `0x1C` | `0x183D39` | - | `0x183D39` | `Triniti2D::CppGameObjectCallBack::OnTimeout` |
| 8 | `0x20` | `0x183D3B` | - | `0x183D3B` | `Triniti2D::CppGameObjectCallBack::OnGameEvent` |

## DinoCap::dinosaurClass

Virtual table at `0x22C700`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0x11991` | - | `0x11991` | `dinosaurClass::~dinosaurClass` |
| 1 | `0x4` | `0x11959` | - | `0x11959` | `dinosaurClass::~dinosaurClass` |
| 2 | `0x8` | `0x183D29` | - | `0x183D29` | `Triniti2D::CppGameObjectCallBack::Attach` |
| 3 | `0xC` | `0x183D2D` | - | `0x183D2D` | `Triniti2D::CppGameObjectCallBack::Detach` |
| 4 | `0x10` | `0x183D33` | - | `0x183D33` | `Triniti2D::CppGameObjectCallBack::OnLoad` |
| 5 | `0x14` | `0x183D35` | - | `0x183D35` | `Triniti2D::CppGameObjectCallBack::OnUnload` |
| 6 | `0x18` | `0x11BA5` | - | `0x11BA5` | [`dinosaurClass::OnUpdate`](../functions/dossiers/dinosaurClass/FUN-011BA4.md) |
| 7 | `0x1C` | `0x11ACD` | - | `0x11ACD` | `dinosaurClass::OnTimeout` |
| 8 | `0x20` | `0x183D3B` | - | `0x183D3B` | `Triniti2D::CppGameObjectCallBack::OnGameEvent` |
| 9 | `0x24` | `0x11AD7` | - | `0x11AD7` | [`dinosaurClass::OnAnimationEnd`](../functions/dossiers/dinosaurClass/FUN-011AD6.md) |
| 10 | `0x28` | `0x1170F` | - | `0x1170F` | [`dinosaurClass::OnAnimationEndAdv`](../functions/dossiers/dinosaurClass/FUN-01170E.md) |
| 11 | `0x2C` | `0x156D5` | - | `0x156D5` | [`dinosaurClass::OnKeyFrame`](../functions/dossiers/dinosaurClass/FUN-0156D4.md) |
| 12 | `0x30` | `0xA017` | - | `0xA017` | `CAObjCallBack::OnKeyFrameAdv` |

## DinoCap::effectClass

Virtual table at `0x22C7C0`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0x16029` | - | `0x16029` | `effectClass::~effectClass` |
| 1 | `0x4` | `0x15FF3` | - | `0x15FF3` | `effectClass::~effectClass` |
| 2 | `0x8` | `0x183D29` | - | `0x183D29` | `Triniti2D::CppGameObjectCallBack::Attach` |
| 3 | `0xC` | `0x183D2D` | - | `0x183D2D` | `Triniti2D::CppGameObjectCallBack::Detach` |
| 4 | `0x10` | `0x183D33` | - | `0x183D33` | `Triniti2D::CppGameObjectCallBack::OnLoad` |
| 5 | `0x14` | `0x183D35` | - | `0x183D35` | `Triniti2D::CppGameObjectCallBack::OnUnload` |
| 6 | `0x18` | `0x167A7` | - | `0x167A7` | [`effectClass::OnUpdate`](../functions/dossiers/effectClass/FUN-0167A6.md) |
| 7 | `0x1C` | `0x183D39` | - | `0x183D39` | `Triniti2D::CppGameObjectCallBack::OnTimeout` |
| 8 | `0x20` | `0x183D3B` | - | `0x183D3B` | `Triniti2D::CppGameObjectCallBack::OnGameEvent` |
| 9 | `0x24` | `0x16AF1` | - | `0x16AF1` | [`effectClass::OnAnimationEnd`](../functions/dossiers/effectClass/FUN-016AF0.md) |
| 10 | `0x28` | `0xA013` | - | `0xA013` | `CAObjCallBack::OnAnimationEndAdv` |
| 11 | `0x2C` | `0xA015` | - | `0xA015` | `CAObjCallBack::OnKeyFrame` |
| 12 | `0x30` | `0xA017` | - | `0xA017` | `CAObjCallBack::OnKeyFrameAdv` |

## DinoCap::effectClassN

Virtual table at `0x22D194`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0x11FB71` | - | `0x11FB71` | `effectClassN::~effectClassN` |
| 1 | `0x4` | `0x11FB0D` | - | `0x11FB0D` | `effectClassN::~effectClassN` |
| 2 | `0x8` | `0x183D29` | - | `0x183D29` | `Triniti2D::CppGameObjectCallBack::Attach` |
| 3 | `0xC` | `0x183D2D` | - | `0x183D2D` | `Triniti2D::CppGameObjectCallBack::Detach` |
| 4 | `0x10` | `0x11790D` | - | `0x11790D` | `CompAnimObj::OnLoad` |
| 5 | `0x14` | `0x183D35` | - | `0x183D35` | `Triniti2D::CppGameObjectCallBack::OnUnload` |
| 6 | `0x18` | `0x11FBDB` | - | `0x11FBDB` | [`effectClassN::OnUpdate`](../functions/dossiers/effectClassN/FUN-11FBDA.md) |
| 7 | `0x1C` | `0x183D39` | - | `0x183D39` | `Triniti2D::CppGameObjectCallBack::OnTimeout` |
| 8 | `0x20` | `0x183D3B` | - | `0x183D3B` | `Triniti2D::CppGameObjectCallBack::OnGameEvent` |
| 9 | `0x24` | `0x1179DD` | - | `0x1179DD` | `CompAnimObj::OnDraw` |
| 10 | `0x28` | `0x11FA81` | - | `0x11FA81` | [`effectClassN::OnAnimationEnd`](../functions/dossiers/effectClassN/FUN-11FA80.md) |
| 11 | `0x2C` | `0x117911` | - | `0x117911` | `CompAnimObj::OnAnimationEndAdv` |
| 12 | `0x30` | `0x117913` | - | `0x117913` | `CompAnimObj::OnKeyFrame` |
| 13 | `0x34` | `0x117915` | - | `0x117915` | `CompAnimObj::OnKeyFrameAdv` |

## DinoCap::eggClass

Virtual table at `0x22D1F4`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0x11FE91` | - | `0x11FE91` | `eggClass::~eggClass` |
| 1 | `0x4` | `0x11FE59` | - | `0x11FE59` | `eggClass::~eggClass` |
| 2 | `0x8` | `0x183D29` | - | `0x183D29` | `Triniti2D::CppGameObjectCallBack::Attach` |
| 3 | `0xC` | `0x183D2D` | - | `0x183D2D` | `Triniti2D::CppGameObjectCallBack::Detach` |
| 4 | `0x10` | `0x11FE1D` | - | `0x11FE1D` | `eggClass::OnLoad` |
| 5 | `0x14` | `0x183D35` | - | `0x183D35` | `Triniti2D::CppGameObjectCallBack::OnUnload` |
| 6 | `0x18` | `0x11FEB7` | - | `0x11FEB7` | [`eggClass::OnUpdate`](../functions/dossiers/eggClass/FUN-11FEB6.md) |
| 7 | `0x1C` | `0x183D39` | - | `0x183D39` | `Triniti2D::CppGameObjectCallBack::OnTimeout` |
| 8 | `0x20` | `0x183D3B` | - | `0x183D3B` | `Triniti2D::CppGameObjectCallBack::OnGameEvent` |
| 9 | `0x24` | `0xA011` | - | `0xA011` | `CAObjCallBack::OnAnimationEnd` |
| 10 | `0x28` | `0xA013` | - | `0xA013` | `CAObjCallBack::OnAnimationEndAdv` |
| 11 | `0x2C` | `0xA015` | - | `0xA015` | `CAObjCallBack::OnKeyFrame` |
| 12 | `0x30` | `0xA017` | - | `0xA017` | `CAObjCallBack::OnKeyFrameAdv` |

## DinoCap::houseClass

Virtual table at `0x22C574`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0x6EC9` | - | `0x6EC9` | `houseClass::~houseClass` |
| 1 | `0x4` | `0x6E91` | - | `0x6E91` | `houseClass::~houseClass` |
| 2 | `0x8` | `0x183D29` | - | `0x183D29` | `Triniti2D::CppGameObjectCallBack::Attach` |
| 3 | `0xC` | `0x183D2D` | - | `0x183D2D` | `Triniti2D::CppGameObjectCallBack::Detach` |
| 4 | `0x10` | `0x72DD` | - | `0x72DD` | [`houseClass::OnLoad`](../functions/dossiers/houseClass/FUN-0072DC.md) |
| 5 | `0x14` | `0x183D35` | - | `0x183D35` | `Triniti2D::CppGameObjectCallBack::OnUnload` |
| 6 | `0x18` | `0xA6CD` | - | `0xA6CD` | [`CAObjCallBack::OnUpdate`](../functions/dossiers/CAObjCallBack/FUN-00A6CC.md) |
| 7 | `0x1C` | `0x71EB` | - | `0x71EB` | [`houseClass::OnTimeout`](../functions/dossiers/houseClass/FUN-0071EA.md) |
| 8 | `0x20` | `0x183D3B` | - | `0x183D3B` | `Triniti2D::CppGameObjectCallBack::OnGameEvent` |
| 9 | `0x24` | `0x6D89` | - | `0x6D89` | [`houseClass::OnAnimationEnd`](../functions/dossiers/houseClass/FUN-006D88.md) |
| 10 | `0x28` | `0xA013` | - | `0xA013` | `CAObjCallBack::OnAnimationEndAdv` |
| 11 | `0x2C` | `0x6E1D` | - | `0x6E1D` | [`houseClass::OnKeyFrame`](../functions/dossiers/houseClass/FUN-006E1C.md) |
| 12 | `0x30` | `0xA017` | - | `0xA017` | `CAObjCallBack::OnKeyFrameAdv` |

## DinoCap::itemClass

Virtual table at `0x22C5D8`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0x97EF` | - | `0x97EF` | `itemClass::~itemClass` |
| 1 | `0x4` | `0x9771` | - | `0x9771` | `itemClass::~itemClass` |
| 2 | `0x8` | `0x183D29` | - | `0x183D29` | `Triniti2D::CppGameObjectCallBack::Attach` |
| 3 | `0xC` | `0x183D2D` | - | `0x183D2D` | `Triniti2D::CppGameObjectCallBack::Detach` |
| 4 | `0x10` | `0x7989` | - | `0x7989` | [`itemClass::OnLoad`](../functions/dossiers/itemClass/FUN-007988.md) |
| 5 | `0x14` | `0x183D35` | - | `0x183D35` | `Triniti2D::CppGameObjectCallBack::OnUnload` |
| 6 | `0x18` | `0x7BBF` | - | `0x7BBF` | [`itemClass::OnUpdate`](../functions/dossiers/itemClass/FUN-007BBE.md) |
| 7 | `0x1C` | `0x183D39` | - | `0x183D39` | `Triniti2D::CppGameObjectCallBack::OnTimeout` |
| 8 | `0x20` | `0x183D3B` | - | `0x183D3B` | `Triniti2D::CppGameObjectCallBack::OnGameEvent` |

## DinoCap::itemsClass

Virtual table at `0x22D0B8`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0x115A5D` | - | `0x115A5D` | `itemsClass::~itemsClass` |
| 1 | `0x4` | `0x115A25` | - | `0x115A25` | `itemsClass::~itemsClass` |
| 2 | `0x8` | `0x183D29` | - | `0x183D29` | `Triniti2D::CppGameObjectCallBack::Attach` |
| 3 | `0xC` | `0x183D2D` | - | `0x183D2D` | `Triniti2D::CppGameObjectCallBack::Detach` |
| 4 | `0x10` | `0x115A67` | - | `0x115A67` | `itemsClass::OnLoad` |
| 5 | `0x14` | `0x183D35` | - | `0x183D35` | `Triniti2D::CppGameObjectCallBack::OnUnload` |
| 6 | `0x18` | `0x115CC5` | - | `0x115CC5` | [`itemsClass::OnUpdate`](../functions/dossiers/itemsClass/FUN-115CC4.md) |
| 7 | `0x1C` | `0x115AFD` | - | `0x115AFD` | [`itemsClass::OnTimeout`](../functions/dossiers/itemsClass/FUN-115AFC.md) |
| 8 | `0x20` | `0x183D3B` | - | `0x183D3B` | `Triniti2D::CppGameObjectCallBack::OnGameEvent` |
| 9 | `0x24` | `0xA011` | - | `0xA011` | `CAObjCallBack::OnAnimationEnd` |
| 10 | `0x28` | `0xA013` | - | `0xA013` | `CAObjCallBack::OnAnimationEndAdv` |
| 11 | `0x2C` | `0xA015` | - | `0xA015` | `CAObjCallBack::OnKeyFrame` |
| 12 | `0x30` | `0xA017` | - | `0xA017` | `CAObjCallBack::OnKeyFrameAdv` |

## DinoCap::jigsawPicClass

Virtual table at `0x22D29C`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0x1246AD` | - | `0x1246AD` | `jigsawPicClass::~jigsawPicClass` |
| 1 | `0x4` | `0x124677` | - | `0x124677` | `jigsawPicClass::~jigsawPicClass` |
| 2 | `0x8` | `0x183D29` | - | `0x183D29` | `Triniti2D::CppGameObjectCallBack::Attach` |
| 3 | `0xC` | `0x183D2D` | - | `0x183D2D` | `Triniti2D::CppGameObjectCallBack::Detach` |
| 4 | `0x10` | `0x183D33` | - | `0x183D33` | `Triniti2D::CppGameObjectCallBack::OnLoad` |
| 5 | `0x14` | `0x183D35` | - | `0x183D35` | `Triniti2D::CppGameObjectCallBack::OnUnload` |
| 6 | `0x18` | `0x124723` | - | `0x124723` | [`jigsawPicClass::OnUpdate`](../functions/dossiers/jigsawPicClass/FUN-124722.md) |
| 7 | `0x1C` | `0x183D39` | - | `0x183D39` | `Triniti2D::CppGameObjectCallBack::OnTimeout` |
| 8 | `0x20` | `0x183D3B` | - | `0x183D3B` | `Triniti2D::CppGameObjectCallBack::OnGameEvent` |
| 9 | `0x24` | `0x12466D` | - | `0x12466D` | `jigsawPicClass::OnAnimationEnd` |
| 10 | `0x28` | `0xA013` | - | `0xA013` | `CAObjCallBack::OnAnimationEndAdv` |
| 11 | `0x2C` | `0xA015` | - | `0xA015` | `CAObjCallBack::OnKeyFrame` |
| 12 | `0x30` | `0xA017` | - | `0xA017` | `CAObjCallBack::OnKeyFrameAdv` |

## DinoCap::lifeModeDu

Virtual table at `0x22CE68`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0xCFA71` | - | `0xCFA71` | `lifeModeDu::~lifeModeDu` |
| 1 | `0x4` | `0xCFA39` | - | `0xCFA39` | `lifeModeDu::~lifeModeDu` |
| 2 | `0x8` | `0x183D29` | - | `0x183D29` | `Triniti2D::CppGameObjectCallBack::Attach` |
| 3 | `0xC` | `0x183D2D` | - | `0x183D2D` | `Triniti2D::CppGameObjectCallBack::Detach` |
| 4 | `0x10` | `0xCF8F5` | - | `0xCF8F5` | `lifeModeDu::OnLoad` |
| 5 | `0x14` | `0x183D35` | - | `0x183D35` | `Triniti2D::CppGameObjectCallBack::OnUnload` |
| 6 | `0x18` | `0xCFAB7` | - | `0xCFAB7` | `lifeModeDu::OnUpdate` |
| 7 | `0x1C` | `0xCF8BD` | - | `0xCF8BD` | `lifeModeDu::OnTimeout` |
| 8 | `0x20` | `0x183D3B` | - | `0x183D3B` | `Triniti2D::CppGameObjectCallBack::OnGameEvent` |
| 9 | `0x24` | `0xA011` | - | `0xA011` | `CAObjCallBack::OnAnimationEnd` |
| 10 | `0x28` | `0xA013` | - | `0xA013` | `CAObjCallBack::OnAnimationEndAdv` |
| 11 | `0x2C` | `0xCF8C1` | - | `0xCF8C1` | `lifeModeDu::OnKeyFrame` |
| 12 | `0x30` | `0xA017` | - | `0xA017` | `CAObjCallBack::OnKeyFrameAdv` |

## DinoCap::lifeModeScene

Virtual table at `0x22CEB0`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0xD521B` | - | `0xD521B` | `lifeModeScene::~lifeModeScene` |
| 1 | `0x4` | `0xD514D` | - | `0xD514D` | `lifeModeScene::~lifeModeScene` |
| 2 | `0x8` | `0x1850C1` | - | `0x1850C1` | `Triniti2D::CppSceneCallBack::Attach` |
| 3 | `0xC` | `0x1850C5` | - | `0x1850C5` | `Triniti2D::CppSceneCallBack::Detach` |
| 4 | `0x10` | `0xE7203` | - | `0xE7203` | [`lifeModeScene::OnLoad`](../functions/dossiers/lifeModeScene/FUN-0E7202.md) |
| 5 | `0x14` | `0xD22D5` | - | `0xD22D5` | `lifeModeScene::OnUnload` |
| 6 | `0x18` | `0xD41BB` | - | `0xD41BB` | [`lifeModeScene::OnUpdate`](../functions/dossiers/lifeModeScene/FUN-0D41BA.md) |
| 7 | `0x1C` | `0x1850D1` | - | `0x1850D1` | `Triniti2D::CppSceneCallBack::OnLateUpdate` |
| 8 | `0x20` | `0xE6551` | - | `0xE6551` | [`lifeModeScene::OnUIEvent`](../functions/dossiers/lifeModeScene/FUN-0E6550.md) |
| 9 | `0x24` | `0xD15E5` | - | `0xD15E5` | [`lifeModeScene::OnTouchEvent`](../functions/dossiers/lifeModeScene/FUN-0D15E4.md) |
| 10 | `0x28` | `0x1850D7` | - | `0x1850D7` | `Triniti2D::CppSceneCallBack::OnCollide` |
| 11 | `0x2C` | `0xD0959` | - | `0xD0959` | [`lifeModeScene::OnSystemEvent`](../functions/dossiers/lifeModeScene/FUN-0D0958.md) |
| 12 | `0x30` | `0xE2A4D` | - | `0xE2A4D` | [`lifeModeScene::OnTimeout`](../functions/dossiers/lifeModeScene/FUN-0E2A4C.md) |
| 13 | `0x34` | `0x1850DD` | - | `0x1850DD` | `Triniti2D::CppSceneCallBack::OnGameEvent` |
| 14 | `0x38` | `0xF0FF9` | - | `0xF0FF9` | [`lifeModeScene::GameClear`](../functions/dossiers/lifeModeScene/FUN-0F0FF8.md) |
| 15 | `0x3C` | `0xD1F91` | - | `0xD1F91` | [`lifeModeScene::UpdateExp`](../functions/dossiers/lifeModeScene/FUN-0D1F90.md) |
| 16 | `0x40` | `0xD5309` | - | `0xD5309` | [`lifeModeScene::CreateEnemy`](../functions/dossiers/lifeModeScene/FUN-0D5308.md) |
| 17 | `0x44` | `0xD14B5` | - | `0xD14B5` | [`lifeModeScene::StartGame`](../functions/dossiers/lifeModeScene/FUN-0D14B4.md) |
| 18 | `0x48` | `0xE232F` | - | `0xE232F` | [`lifeModeScene::StartGame_N`](../functions/dossiers/lifeModeScene/FUN-0E232E.md) |
| 19 | `0x4C` | `0xF13B5` | - | `0xF13B5` | [`lifeModeScene::LevelEnter`](../functions/dossiers/lifeModeScene/FUN-0F13B4.md) |
| 20 | `0x50` | `0xF17E7` | - | `0xF17E7` | [`lifeModeScene::LevelLeave`](../functions/dossiers/lifeModeScene/FUN-0F17E6.md) |
| 21 | `0x54` | `0xD4B51` | - | `0xD4B51` | `lifeModeScene::GameOverEnd` |

## DinoCap::lifeModeTools

Virtual table at `0x22CF1C`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0xF28A9` | - | `0xF28A9` | `lifeModeTools::~lifeModeTools` |
| 1 | `0x4` | `0xF2871` | - | `0xF2871` | `lifeModeTools::~lifeModeTools` |
| 2 | `0x8` | `0x183D29` | - | `0x183D29` | `Triniti2D::CppGameObjectCallBack::Attach` |
| 3 | `0xC` | `0x183D2D` | - | `0x183D2D` | `Triniti2D::CppGameObjectCallBack::Detach` |
| 4 | `0x10` | `0xF2465` | - | `0xF2465` | `lifeModeTools::OnLoad` |
| 5 | `0x14` | `0x183D35` | - | `0x183D35` | `Triniti2D::CppGameObjectCallBack::OnUnload` |
| 6 | `0x18` | `0xF28FF` | - | `0xF28FF` | `lifeModeTools::OnUpdate` |
| 7 | `0x1C` | `0xF2E29` | - | `0xF2E29` | `lifeModeTools::OnTimeout` |
| 8 | `0x20` | `0x183D3B` | - | `0x183D3B` | `Triniti2D::CppGameObjectCallBack::OnGameEvent` |
| 9 | `0x24` | `0xA011` | - | `0xA011` | `CAObjCallBack::OnAnimationEnd` |
| 10 | `0x28` | `0xA013` | - | `0xA013` | `CAObjCallBack::OnAnimationEndAdv` |
| 11 | `0x2C` | `0xA015` | - | `0xA015` | `CAObjCallBack::OnKeyFrame` |
| 12 | `0x30` | `0xA017` | - | `0xA017` | `CAObjCallBack::OnKeyFrameAdv` |

## DinoCap::loadEffClass

Virtual table at `0x22C748`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0x15FA9` | - | `0x15FA9` | `loadEffClass::~loadEffClass` |
| 1 | `0x4` | `0x15F71` | - | `0x15F71` | `loadEffClass::~loadEffClass` |
| 2 | `0x8` | `0x183D29` | - | `0x183D29` | `Triniti2D::CppGameObjectCallBack::Attach` |
| 3 | `0xC` | `0x183D2D` | - | `0x183D2D` | `Triniti2D::CppGameObjectCallBack::Detach` |
| 4 | `0x10` | `0x183D33` | - | `0x183D33` | `Triniti2D::CppGameObjectCallBack::OnLoad` |
| 5 | `0x14` | `0x183D35` | - | `0x183D35` | `Triniti2D::CppGameObjectCallBack::OnUnload` |
| 6 | `0x18` | `0x15F49` | - | `0x15F49` | [`loadEffClass::OnUpdate`](../functions/dossiers/loadEffClass/FUN-015F48.md) |
| 7 | `0x1C` | `0x183D39` | - | `0x183D39` | `Triniti2D::CppGameObjectCallBack::OnTimeout` |
| 8 | `0x20` | `0x183D3B` | - | `0x183D3B` | `Triniti2D::CppGameObjectCallBack::OnGameEvent` |
| 9 | `0x24` | `0x15E81` | - | `0x15E81` | [`loadEffClass::OnAnimationEnd`](../functions/dossiers/loadEffClass/FUN-015E80.md) |
| 10 | `0x28` | `0xA013` | - | `0xA013` | `CAObjCallBack::OnAnimationEndAdv` |
| 11 | `0x2C` | `0xA015` | - | `0xA015` | `CAObjCallBack::OnKeyFrame` |
| 12 | `0x30` | `0xA017` | - | `0xA017` | `CAObjCallBack::OnKeyFrameAdv` |

## DinoCap::mainScene

Virtual table at `0x22C820`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0x9681F` | - | `0x9681F` | `mainScene::~mainScene` |
| 1 | `0x4` | `0x971D5` | - | `0x971D5` | `mainScene::~mainScene` |
| 2 | `0x8` | `0x1850C1` | - | `0x1850C1` | `Triniti2D::CppSceneCallBack::Attach` |
| 3 | `0xC` | `0x1850C5` | - | `0x1850C5` | `Triniti2D::CppSceneCallBack::Detach` |
| 4 | `0x10` | `0xA7435` | - | `0xA7435` | [`mainScene::OnLoad`](../functions/dossiers/mainScene/FUN-0A7434.md) |
| 5 | `0x14` | `0x98AD1` | - | `0x98AD1` | `mainScene::OnUnload` |
| 6 | `0x18` | `0x979D3` | - | `0x979D3` | [`mainScene::OnUpdate`](../functions/dossiers/mainScene/FUN-0979D2.md) |
| 7 | `0x1C` | `0x1850D1` | - | `0x1850D1` | `Triniti2D::CppSceneCallBack::OnLateUpdate` |
| 8 | `0x20` | `0xA69AF` | - | `0xA69AF` | [`mainScene::OnUIEvent`](../functions/dossiers/mainScene/FUN-0A69AE.md) |
| 9 | `0x24` | `0x96915` | - | `0x96915` | [`mainScene::OnTouchEvent`](../functions/dossiers/mainScene/FUN-096914.md) |
| 10 | `0x28` | `0x1850D7` | - | `0x1850D7` | `Triniti2D::CppSceneCallBack::OnCollide` |
| 11 | `0x2C` | `0x9636B` | - | `0x9636B` | [`mainScene::OnSystemEvent`](../functions/dossiers/mainScene/FUN-09636A.md) |

## DinoCap::menuObj

Virtual table at `0x22CC80`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0xCB4D9` | - | `0xCB4D9` | `menuObj::~menuObj` |
| 1 | `0x4` | `0xCB4A1` | - | `0xCB4A1` | `menuObj::~menuObj` |
| 2 | `0x8` | `0x183D29` | - | `0x183D29` | `Triniti2D::CppGameObjectCallBack::Attach` |
| 3 | `0xC` | `0x183D2D` | - | `0x183D2D` | `Triniti2D::CppGameObjectCallBack::Detach` |
| 4 | `0x10` | `0x183D33` | - | `0x183D33` | `Triniti2D::CppGameObjectCallBack::OnLoad` |
| 5 | `0x14` | `0x183D35` | - | `0x183D35` | `Triniti2D::CppGameObjectCallBack::OnUnload` |
| 6 | `0x18` | `0xCB497` | - | `0xCB497` | [`menuObj::OnUpdate`](../functions/dossiers/menuObj/FUN-0CB496.md) |
| 7 | `0x1C` | `0x183D39` | - | `0x183D39` | `Triniti2D::CppGameObjectCallBack::OnTimeout` |
| 8 | `0x20` | `0x183D3B` | - | `0x183D3B` | `Triniti2D::CppGameObjectCallBack::OnGameEvent` |
| 9 | `0x24` | `0xA011` | - | `0xA011` | `CAObjCallBack::OnAnimationEnd` |
| 10 | `0x28` | `0xA013` | - | `0xA013` | `CAObjCallBack::OnAnimationEndAdv` |
| 11 | `0x2C` | `0xCB0EB` | - | `0xCB0EB` | [`menuObj::OnKeyFrame`](../functions/dossiers/menuObj/FUN-0CB0EA.md) |
| 12 | `0x30` | `0xA017` | - | `0xA017` | `CAObjCallBack::OnKeyFrameAdv` |

## DinoCap::menuObjN

Virtual table at `0x22CC28`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0xCB465` | - | `0xCB465` | `menuObjN::~menuObjN` |
| 1 | `0x4` | `0xCB401` | - | `0xCB401` | `menuObjN::~menuObjN` |
| 2 | `0x8` | `0x183D29` | - | `0x183D29` | `Triniti2D::CppGameObjectCallBack::Attach` |
| 3 | `0xC` | `0x183D2D` | - | `0x183D2D` | `Triniti2D::CppGameObjectCallBack::Detach` |
| 4 | `0x10` | `0x11790D` | - | `0x11790D` | `CompAnimObj::OnLoad` |
| 5 | `0x14` | `0x183D35` | - | `0x183D35` | `Triniti2D::CppGameObjectCallBack::OnUnload` |
| 6 | `0x18` | `0xCB3E1` | - | `0xCB3E1` | `menuObjN::OnUpdate` |
| 7 | `0x1C` | `0x183D39` | - | `0x183D39` | `Triniti2D::CppGameObjectCallBack::OnTimeout` |
| 8 | `0x20` | `0x183D3B` | - | `0x183D3B` | `Triniti2D::CppGameObjectCallBack::OnGameEvent` |
| 9 | `0x24` | `0x1179DD` | - | `0x1179DD` | `CompAnimObj::OnDraw` |
| 10 | `0x28` | `0x11790F` | - | `0x11790F` | `CompAnimObj::OnAnimationEnd` |
| 11 | `0x2C` | `0x117911` | - | `0x117911` | `CompAnimObj::OnAnimationEndAdv` |
| 12 | `0x30` | `0x117913` | - | `0x117913` | `CompAnimObj::OnKeyFrame` |
| 13 | `0x34` | `0x117915` | - | `0x117915` | `CompAnimObj::OnKeyFrameAdv` |
| 14 | `0x38` | `0xCB4FF` | - | `0xCB4FF` | [`menuObjN::OnKeyFrame`](../functions/dossiers/menuObjN/FUN-0CB4FE.md) |

## DinoCap::menuScene

Virtual table at `0x22CBE8`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0xCAF67` | - | `0xCAF67` | `menuScene::~menuScene` |
| 1 | `0x4` | `0xCB7F5` | - | `0xCB7F5` | `menuScene::~menuScene` |
| 2 | `0x8` | `0x1850C1` | - | `0x1850C1` | `Triniti2D::CppSceneCallBack::Attach` |
| 3 | `0xC` | `0x1850C5` | - | `0x1850C5` | `Triniti2D::CppSceneCallBack::Detach` |
| 4 | `0x10` | `0xCC019` | - | `0xCC019` | [`menuScene::OnLoad`](../functions/dossiers/menuScene/FUN-0CC018.md) |
| 5 | `0x14` | `0xCBD93` | - | `0xCBD93` | `menuScene::OnUnload` |
| 6 | `0x18` | `0xCBAF5` | - | `0xCBAF5` | [`menuScene::OnUpdate`](../functions/dossiers/menuScene/FUN-0CBAF4.md) |
| 7 | `0x1C` | `0x1850D1` | - | `0x1850D1` | `Triniti2D::CppSceneCallBack::OnLateUpdate` |
| 8 | `0x20` | `0xCA20D` | - | `0xCA20D` | [`menuScene::OnUIEvent`](../functions/dossiers/menuScene/FUN-0CA20C.md) |
| 9 | `0x24` | `0xCC8B3` | - | `0xCC8B3` | [`menuScene::OnTouchEvent`](../functions/dossiers/menuScene/FUN-0CC8B2.md) |
| 10 | `0x28` | `0x1850D7` | - | `0x1850D7` | `Triniti2D::CppSceneCallBack::OnCollide` |
| 11 | `0x2C` | `0x1850D9` | - | `0x1850D9` | `Triniti2D::CppSceneCallBack::OnSystemEvent` |
| 12 | `0x30` | `0xCD1E3` | - | `0xCD1E3` | [`menuScene::OnTimeout`](../functions/dossiers/menuScene/FUN-0CD1E2.md) |
| 13 | `0x34` | `0x1850DD` | - | `0x1850DD` | `Triniti2D::CppSceneCallBack::OnGameEvent` |

## DinoCap::particleClass

Virtual table at `0x22C604`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0x7479` | - | `0x7479` | `particleClass::~particleClass` |
| 1 | `0x4` | `0x7441` | - | `0x7441` | `particleClass::~particleClass` |
| 2 | `0x8` | `0x183D29` | - | `0x183D29` | `Triniti2D::CppGameObjectCallBack::Attach` |
| 3 | `0xC` | `0x183D2D` | - | `0x183D2D` | `Triniti2D::CppGameObjectCallBack::Detach` |
| 4 | `0x10` | `0x183D33` | - | `0x183D33` | `Triniti2D::CppGameObjectCallBack::OnLoad` |
| 5 | `0x14` | `0x183D35` | - | `0x183D35` | `Triniti2D::CppGameObjectCallBack::OnUnload` |
| 6 | `0x18` | `0x7DBD` | - | `0x7DBD` | [`particleClass::OnUpdate`](../functions/dossiers/particleClass/FUN-007DBC.md) |
| 7 | `0x1C` | `0x183D39` | - | `0x183D39` | `Triniti2D::CppGameObjectCallBack::OnTimeout` |
| 8 | `0x20` | `0x183D3B` | - | `0x183D3B` | `Triniti2D::CppGameObjectCallBack::OnGameEvent` |

## DinoCap::pauseCtrl

Virtual table at `0x22CDAC`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0xCED01` | - | `0xCED01` | `pauseCtrl::~pauseCtrl` |
| 1 | `0x4` | `0xCEA9D` | - | `0xCEA9D` | `pauseCtrl::~pauseCtrl` |
| 2 | `0x8` | `0xCF05F` | - | `0xCF05F` | [`pauseCtrl::Initialize`](../functions/dossiers/pauseCtrl/FUN-0CF05E.md) |
| 3 | `0xC` | `0x17F18F` | - | `0x17F18F` | `Triniti2D::UIImage::SetRect` |
| 4 | `0x10` | `0x17E673` | - | `0x17E673` | `Triniti2D::UIControl::GetRect` |
| 5 | `0x14` | `0x17E683` | - | `0x17E683` | `Triniti2D::UIControl::SetRectPosition` |
| 6 | `0x18` | `0x17F063` | - | `0x17F063` | `Triniti2D::UIControl::GetRectPosition` |
| 7 | `0x1C` | `0x17E6AD` | - | `0x17E6AD` | `Triniti2D::UIControl::SetRectSize` |
| 8 | `0x20` | `0x17F045` | - | `0x17F045` | `Triniti2D::UIControl::GetRectSize` |
| 9 | `0x24` | `0x17E6D7` | - | `0x17E6D7` | `Triniti2D::UIControl::SetVisible` |
| 10 | `0x28` | `0x17E6DD` | - | `0x17E6DD` | `Triniti2D::UIControl::GetVisible` |
| 11 | `0x2C` | `0x17E6E3` | - | `0x17E6E3` | `Triniti2D::UIControl::SetEnable` |
| 12 | `0x30` | `0x17E6E9` | - | `0x17E6E9` | `Triniti2D::UIControl::GetEnable` |
| 13 | `0x34` | `0x17F249` | - | `0x17F249` | `Triniti2D::UIControlVisible::SetClip` |
| 14 | `0x38` | `0x17F209` | - | `0x17F209` | `Triniti2D::UIControlVisible::ClearClip` |
| 15 | `0x3C` | `0x17E70D` | - | `0x17E70D` | `Triniti2D::UIControl::PtInRect` |
| 16 | `0x40` | `0x17F081` | - | `0x17F081` | `Triniti2D::UIControl::FireUIEvent` |
| 17 | `0x44` | `0xCE8FD` | - | `0xCE8FD` | `pauseCtrl::Draw` |
| 18 | `0x48` | `0xCE84D` | - | `0xCE84D` | [`pauseCtrl::Update`](../functions/dossiers/pauseCtrl/FUN-0CE84C.md) |
| 19 | `0x4C` | `0xCE891` | - | `0xCE891` | [`pauseCtrl::HandleTouch`](../functions/dossiers/pauseCtrl/FUN-0CE890.md) |
| 20 | `0x50` | `0xCE171` | - | `0xCE171` | `pauseCtrl::GetTextureFrame` |
| 21 | `0x54` | `0xCE195` | - | `0xCE195` | `pauseCtrl::GetFont` |
| 22 | `0x58` | `0xCE1B9` | - | `0xCE1B9` | `pauseCtrl::GetFont` |
| 23 | `0x5C` | `0xCE5C1` | - | `0xCE5C1` | [`pauseCtrl::SendUIEvent`](../functions/dossiers/pauseCtrl/FUN-0CE5C0.md) |
| 24 | `0x60` | `0xCE1DD` | - | `0xCE1DD` | `pauseCtrl::AddUISprite` |

## DinoCap::playerClass

Virtual table at `0x22C874`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0xBBDCB` | - | `0xBBDCB` | `playerClass::~playerClass` |
| 1 | `0x4` | `0xBB795` | - | `0xBB795` | `playerClass::~playerClass` |
| 2 | `0x8` | `0x183D29` | - | `0x183D29` | `Triniti2D::CppGameObjectCallBack::Attach` |
| 3 | `0xC` | `0x183D2D` | - | `0x183D2D` | `Triniti2D::CppGameObjectCallBack::Detach` |
| 4 | `0x10` | `0xBC6BB` | - | `0xBC6BB` | [`playerClass::OnLoad`](../functions/dossiers/playerClass/FUN-0BC6BA.md) |
| 5 | `0x14` | `0xB88B1` | - | `0xB88B1` | `playerClass::OnUnload` |
| 6 | `0x18` | `0xBB6CD` | - | `0xBB6CD` | [`playerClass::OnUpdate`](../functions/dossiers/playerClass/FUN-0BB6CC.md) |
| 7 | `0x1C` | `0xBB539` | - | `0xBB539` | `playerClass::OnTimeout` |
| 8 | `0x20` | `0x183D3B` | - | `0x183D3B` | `Triniti2D::CppGameObjectCallBack::OnGameEvent` |
| 9 | `0x24` | `0xBD979` | - | `0xBD979` | [`playerClass::OnAnimationEnd`](../functions/dossiers/playerClass/FUN-0BD978.md) |
| 10 | `0x28` | `0xBC223` | - | `0xBC223` | [`playerClass::OnAnimationEndAdv`](../functions/dossiers/playerClass/FUN-0BC222.md) |
| 11 | `0x2C` | `0xBF407` | - | `0xBF407` | [`playerClass::OnKeyFrame`](../functions/dossiers/playerClass/FUN-0BF406.md) |
| 12 | `0x30` | `0xC076B` | - | `0xC076B` | [`playerClass::OnKeyFrameAdv`](../functions/dossiers/playerClass/FUN-0C076A.md) |

## DinoCap::playerNewAniClass

Virtual table at `0x22D2E4`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0x1260A5` | - | `0x1260A5` | `playerNewAniClass::~playerNewAniClass` |
| 1 | `0x4` | `0x125EB9` | - | `0x125EB9` | `playerNewAniClass::~playerNewAniClass` |
| 2 | `0x8` | `0x183D29` | - | `0x183D29` | `Triniti2D::CppGameObjectCallBack::Attach` |
| 3 | `0xC` | `0x183D2D` | - | `0x183D2D` | `Triniti2D::CppGameObjectCallBack::Detach` |
| 4 | `0x10` | `0x125157` | - | `0x125157` | [`playerNewAniClass::OnLoad`](../functions/dossiers/playerNewAniClass/FUN-125156.md) |
| 5 | `0x14` | `0x183D35` | - | `0x183D35` | `Triniti2D::CppGameObjectCallBack::OnUnload` |
| 6 | `0x18` | `0x127841` | - | `0x127841` | [`playerNewAniClass::OnUpdate`](../functions/dossiers/playerNewAniClass/FUN-127840.md) |
| 7 | `0x1C` | `0x127DAD` | - | `0x127DAD` | [`playerNewAniClass::OnTimeout`](../functions/dossiers/playerNewAniClass/FUN-127DAC.md) |
| 8 | `0x20` | `0x183D3B` | - | `0x183D3B` | `Triniti2D::CppGameObjectCallBack::OnGameEvent` |
| 9 | `0x24` | `0x1179DD` | - | `0x1179DD` | `CompAnimObj::OnDraw` |
| 10 | `0x28` | `0x12AA2D` | - | `0x12AA2D` | [`playerNewAniClass::OnAnimationEnd`](../functions/dossiers/playerNewAniClass/FUN-12AA2C.md) |
| 11 | `0x2C` | `0x1261D3` | - | `0x1261D3` | [`playerNewAniClass::OnAnimationEndAdv`](../functions/dossiers/playerNewAniClass/FUN-1261D2.md) |
| 12 | `0x30` | `0x129181` | - | `0x129181` | [`playerNewAniClass::OnKeyFrame`](../functions/dossiers/playerNewAniClass/FUN-129180.md) |
| 13 | `0x34` | `0x12BD45` | - | `0x12BD45` | [`playerNewAniClass::OnKeyFrameAdv`](../functions/dossiers/playerNewAniClass/FUN-12BD44.md) |

## DinoCap::selPlayerClass

Virtual table at `0x22CFB4`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0x10FD43` | - | `0x10FD43` | `selPlayerClass::~selPlayerClass` |
| 1 | `0x4` | `0x10F6F5` | - | `0x10F6F5` | `selPlayerClass::~selPlayerClass` |
| 2 | `0x8` | `0x183D29` | - | `0x183D29` | `Triniti2D::CppGameObjectCallBack::Attach` |
| 3 | `0xC` | `0x183D2D` | - | `0x183D2D` | `Triniti2D::CppGameObjectCallBack::Detach` |
| 4 | `0x10` | `0x10E025` | - | `0x10E025` | [`selPlayerClass::OnLoad`](../functions/dossiers/selPlayerClass/FUN-10E024.md) |

## DinoCap::selectScene

Virtual table at `0x22CF64`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0xF6B23` | - | `0xF6B23` | `selectScene::~selectScene` |
| 1 | `0x4` | `0xF6B2D` | - | `0xF6B2D` | `selectScene::~selectScene` |
| 2 | `0x8` | `0x1850C1` | - | `0x1850C1` | `Triniti2D::CppSceneCallBack::Attach` |
| 3 | `0xC` | `0x1850C5` | - | `0x1850C5` | `Triniti2D::CppSceneCallBack::Detach` |
| 4 | `0x10` | `0x10600B` | - | `0x10600B` | [`selectScene::OnLoad`](../functions/dossiers/selectScene/FUN-10600A.md) |
| 5 | `0x14` | `0xF5ECD` | - | `0xF5ECD` | `selectScene::OnUnload` |
| 6 | `0x18` | `0xF93C5` | - | `0xF93C5` | [`selectScene::OnUpdate`](../functions/dossiers/selectScene/FUN-0F93C4.md) |
| 7 | `0x1C` | `0x1850D1` | - | `0x1850D1` | `Triniti2D::CppSceneCallBack::OnLateUpdate` |
| 8 | `0x20` | `0x10B93B` | - | `0x10B93B` | [`selectScene::OnUIEvent`](../functions/dossiers/selectScene/FUN-10B93A.md) |
| 9 | `0x24` | `0xF57BD` | - | `0xF57BD` | [`selectScene::OnTouchEvent`](../functions/dossiers/selectScene/FUN-0F57BC.md) |
| 10 | `0x28` | `0x1850D7` | - | `0x1850D7` | `Triniti2D::CppSceneCallBack::OnCollide` |
| 11 | `0x2C` | `0x1850D9` | - | `0x1850D9` | `Triniti2D::CppSceneCallBack::OnSystemEvent` |
| 12 | `0x30` | `0xFA3D3` | - | `0xFA3D3` | [`selectScene::OnTimeout`](../functions/dossiers/selectScene/FUN-0FA3D2.md) |
| 13 | `0x34` | `0x1850DD` | - | `0x1850DD` | `Triniti2D::CppSceneCallBack::OnGameEvent` |

## DinoCap::testApp

Virtual table at `0x22C8BC`.

| Slot | Offset | File word | Relocation | Resolved word | Target |
|---:|---|---|---|---|---|
| 0 | `0x0` | `0xC0BDB` | - | `0xC0BDB` | `testApp::~testApp` |
| 1 | `0x4` | `0xC09C5` | - | `0xC09C5` | `testApp::~testApp` |
| 2 | `0x8` | `0x183C75` | - | `0x183C75` | `Triniti2D::CppGameCallBack::Attach` |
| 3 | `0xC` | `0x183C79` | - | `0x183C79` | `Triniti2D::CppGameCallBack::Detach` |
| 4 | `0x10` | `0xC0C8D` | - | `0xC0C8D` | `testApp::OnLoad` |
| 5 | `0x14` | `0xC0C21` | - | `0xC0C21` | `testApp::OnUnload` |
| 6 | `0x18` | `0xC0999` | - | `0xC0999` | `testApp::OnBackground` |
| 7 | `0x1C` | `0xC0BE5` | - | `0xC0BE5` | `testApp::OnForeground` |

