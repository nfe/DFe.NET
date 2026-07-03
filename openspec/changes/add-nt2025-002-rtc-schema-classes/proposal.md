## Why

A NF-e da Reforma Tributária (RTC — NT 2025.002) introduz grupos e campos novos no leiaute
que a biblioteca **DFe.NET (Zeus)** ainda não modela. O consumidor `dfetech-product-invoice-api`
(PR nfe/dfetech-product-invoice-api#265) precisa dessas classes para emitir os grupos na
conversão XML.

Escopo desta change é limitado ao que **está presente no schema fornecido** — `PL_010e_v1.01`
(`DFeTiposBasicos_v1.00.xsd` + `leiauteNFe_v4.00.xsd`), nível RTC **v1.40**. Ficam **de fora**
(não existem neste XSD): o **monofásico reformulado v1.50** (Ad Rem × Ad Valorem, `gpBioDiferenca`)
e o **`ISUFemit`** — serão tratados quando o pacote v1.50 oficial estiver disponível.

## What Changes

Adicionar ao Zeus as classes/campos abaixo, mapeando 1:1 o XSD do PL_010e_v1.01, com os
atributos de serialização XML no padrão da lib:

### 1. Grupo Imposto Seletivo — `IS` (tipo `TIS`)
Novo grupo, irmão de `IBSCBS` dentro de `imposto` (det/imposto): `CSTIS` (TCST),
`cClassTribIS` (opcional), `vBCIS`, `pIS`, `adRemIS` (opcional), `uTrib` (opcional), `qTrib`, `vIS`.

### 2. `cIndOp` — código indicador do local da operação de fornecimento
Campo opcional no produto/item (`prod`), modelo 55 (vedado NFC-e).

### 3. `refDFeAnt` — referência a DF-e anterior (Compras Governamentais, BB05)
Campo (`TChDFeRTC`) no grupo de Compras Governamentais.

### 4. `gALCZFMCBS` — ALC/ZFM alíquota zero CBS (UB66a)
Grupo sob IBS/CBS: `tpALCZFMCBS`, `nProcSUFRAMA`, `pAliqEfetRegCBS`, `vTribRegCBS`.

Cada item inclui: classe(s) de domínio, propriedades com `[XmlElement]`/`ShouldSerialize*` no
padrão Zeus, e o encaixe no agregado pai (imposto/prod/IBSCBS/CompraGov). Sem quebrar a
serialização atual (campos novos são opcionais / `minOccurs=0`).

## Capabilities

### New Capabilities
- `nfe-rtc-schema-classes`: classes de domínio + serialização dos grupos RTC do PL_010e_v1.01
  (IS, cIndOp, refDFeAnt, gALCZFMCBS) ausentes no Zeus.

### Modified Capabilities
_Nenhuma (adição de campos opcionais; não altera grupos existentes além do encaixe)._

## Impact

**Código (DFe.NET / NFe.Classes):**
- Novo: `Informacoes/Detalhe/Tributacao/Compartilhado/IS/*` (grupo IS/TIS).
- Novo: `gALCZFMCBS` sob `Informacoes/Detalhe/Tributacao/Compartilhado/InformacoesIbsCbs/`.
- `prod` (produto) — campo `cIndOp`.
- Grupo de Compras Governamentais — campo `refDFeAnt`.
- `imposto.cs` — encaixe do grupo `IS`.

**Compatibilidade:** aditivo; todos os campos são opcionais (`minOccurs=0`) e não aparecem no
XML quando nulos (`ShouldSerialize*`). Não altera a serialização de notas sem RTC.

**Fora de escopo:** monofásico reformulado v1.50, `gpBioDiferenca`, `ISUFemit` (ausentes no
PL_010e_v1.01). Ver nfe/dfetech-product-invoice-api#265 (Fase 3 e tarefa 2.3).

## Rastreabilidade
NT 2025.002-RTC · schema `PL_010e_v1.01` (`DFeTiposBasicos_v1.00.xsd`, `leiauteNFe_v4.00.xsd`).
Consumidor: nfe/dfetech-product-invoice-api#265.
