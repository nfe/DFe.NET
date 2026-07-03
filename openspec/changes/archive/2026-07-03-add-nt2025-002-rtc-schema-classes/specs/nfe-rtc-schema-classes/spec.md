## ADDED Requirements

### Requirement: grupo Imposto Seletivo (IS)
A biblioteca SHALL modelar o grupo `IS` (tipo `TIS`) do schema PL_010e_v1.01 dentro de
`det/imposto`, com os campos na ordem do XSD: `CSTIS`, `cClassTribIS` (opcional), `vBCIS`,
`pIS`, `adRemIS` (opcional), `uTrib` (opcional), `qTrib`, `vIS`. Campos `minOccurs=0` SHALL usar
`ShouldSerialize*` e não emitir a tag quando nulos.

#### Scenario: emissão do grupo IS
- **GIVEN** um item com Imposto Seletivo preenchido (`CSTIS`, `vBCIS`, `pIS`, `qTrib`, `vIS`)
- **WHEN** a NF-e é serializada
- **THEN** o XML contém `det/imposto/IS` com os campos preenchidos, na ordem do XSD
- **AND** `cClassTribIS`/`adRemIS`/`uTrib` só aparecem quando preenchidos

### Requirement: campo cIndOp no produto
O `produto` (`prod`) SHALL expor o campo opcional `cIndOp` (código indicador do local da
operação de fornecimento), emitido apenas quando preenchido.

#### Scenario: cIndOp opcional
- **GIVEN** um produto sem `cIndOp`
- **WHEN** a NF-e é serializada
- **THEN** a tag `cIndOp` não aparece no XML

### Requirement: campo refDFeAnt em Compras Governamentais
O grupo de Compras Governamentais SHALL expor o campo opcional `refDFeAnt` (`TChDFeRTC`),
emitido apenas quando preenchido.

#### Scenario: refDFeAnt opcional
- **GIVEN** uma operação de compra governamental com `refDFeAnt` preenchido
- **WHEN** a NF-e é serializada
- **THEN** o XML contém `refDFeAnt` no grupo de Compras Governamentais

### Requirement: grupo gALCZFMCBS (ALC/ZFM alíquota zero CBS)
A biblioteca SHALL modelar o grupo `gALCZFMCBS` sob IBS/CBS, com `tpALCZFMCBS`, `nProcSUFRAMA`,
`pAliqEfetRegCBS`, `vTribRegCBS`, emitido apenas quando aplicável.

#### Scenario: emissão do gALCZFMCBS
- **GIVEN** uma operação ALC/ZFM com alíquota zero de CBS e `gALCZFMCBS` preenchido
- **WHEN** a NF-e é serializada
- **THEN** o XML contém `gALCZFMCBS` com os campos na ordem do XSD

### Requirement: não regressão da serialização atual
Notas fiscais **sem** os grupos novos SHALL produzir XML idêntico ao anterior a esta change
(nenhuma tag nova emitida quando os campos são nulos).

#### Scenario: nota sem grupos RTC novos
- **GIVEN** uma NF-e sem IS, sem `cIndOp`, sem `refDFeAnt` e sem `gALCZFMCBS`
- **WHEN** a NF-e é serializada
- **THEN** o XML não contém nenhuma dessas tags novas
