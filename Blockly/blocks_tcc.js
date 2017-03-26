


// This file was automatically generated.  Do not modify.

'use strict';

goog.provide('Blockly.Msg.pt.br');

goog.require('Blockly.Msg');

//Blockly.JavaScriptrobôt = [];

Blockly.Blocks['while_tcc'] = {
    init: function () {
        this.setHelpUrl('http://www.example.com/');
        this.setColour(260);
        this.appendDummyInput()
            .appendField("enquanto");
        this.appendValueInput("condition")
            .setCheck("Boolean");
        this.appendStatementInput("do")
            .appendField("Faça")
            .setCheck("null");
        this.appendDummyInput()
            .appendField("Depois Execute")
            .setAlign(Blockly.ALIGN_LEFT);
        this.appendStatementInput("doAfter")
            .setCheck("null");
        //this.setInputsInline(true);
        this.setPreviousStatement(true, "null");
        //this.setNextStatement(true, "null");
        this.setTooltip('Executa em laço as instruções contidas no campo "Faça" e após a condição se tornar falsa executa o "Depois Execute".');
    }

};

Blockly.JavaScript['while_tcc'] = function (block) {
    var value_condition = Blockly.JavaScript.valueToCode(block, 'condition', Blockly.JavaScript.ORDER_ATOMIC);
    var statements_do = Blockly.JavaScript.statementToCode(block, 'do');
    var statements_do_after = Blockly.JavaScript.statementToCode(block, 'doAfter');
    // TODO: Assemble JavaScript into code variable.
    var code = "app.robôt.assign_loop('while','"+value_condition+"',function(){\n"+statements_do+"},function(){\n"+statements_do_after+"});\n";
    return code;
};

Blockly.Blocks['sleep'] = {
    init: function () {
        this.setHelpUrl('http://www.example.com/');
        this.setColour(260);
        this.appendDummyInput()
            .appendField("Dorme");
        this.appendValueInput("tempo")
            .setCheck("Number")
            .setAlign(Blockly.ALIGN_RIGHT);
        this.appendDummyInput()
            .appendField("ms");
        this.appendDummyInput()
            .appendField("Depois Execute")
            .setAlign(Blockly.ALIGN_LEFT);
        this.appendStatementInput("do")
            .setCheck("null");
        this.setInputsInline(true);
        this.setPreviousStatement(true, "null");
        //this.setNextStatement(true, "null");
        this.setTooltip('Faz com que o robô suspenda suas atividades por um determinado tempo.');
    }

};

Blockly.JavaScript['sleep'] = function (block) {
    var value_time = Blockly.JavaScript.valueToCode(block, 'tempo', Blockly.JavaScript.ORDER_ATOMIC);
    var statements_do = Blockly.JavaScript.statementToCode(block, 'do');
    // TODO: Assemble JavaScript into code variable.
    var code = "window.setTimeout(function(){\n"+statements_do+"\n},"+value_time+");\n";
    return code;
};

Blockly.Blocks['velocidade'] = {
    init: function () {
        this.setHelpUrl('');
        this.setColour(260);
        this.appendDummyInput()
          .appendField('Velocidade ');
        this.appendValueInput('esquerda')
            .appendField('esquerda')
            .setCheck('Number')
            .setAlign(Blockly.ALIGN_RIGHT);
        this.appendValueInput('direita')
            .appendField('direita')
            .setCheck('Number')
            .setAlign(Blockly.ALIGN_RIGHT);
        this.setInputsInline(true);
        this.setPreviousStatement(true, "null");
        this.setNextStatement(true, "null");
        this.setTooltip('Esta função muda a velocidade dos motores direito e esquerdo do robô.');
    }
};

Blockly.JavaScript['velocidade'] = function (block) {
    var vel_esq = Blockly.JavaScript.valueToCode(block, 'esquerda', Blockly.JavaScript.ORDER_ATOMIC);
    var vel_dir = Blockly.JavaScript.valueToCode(block, 'direita', Blockly.JavaScript.ORDER_ATOMIC);
    // TODO: Assemble JavaScript into code variable.
    var code = 'app.robôt.assign_action(set_speed,[' + vel_esq + ',' + vel_dir + ']);\n';
    return code;
};

Blockly.Blocks['mov_virar'] = {
  init: function() {
    this.setHelpUrl('');
    this.setColour(260);
    this.appendDummyInput()
      .appendField('virar');
    this.appendDummyInput()
        .appendField(new Blockly.FieldAngle("90"), "angulo");
    this.appendDummyInput()
        .setAlign(Blockly.ALIGN_CENTRE)
        .appendField(new Blockly.FieldDropdown([["esquerda", "esquerda"], ["direita", "direita"]]), "direcao");
    this.setInputsInline(true);
    this.setPreviousStatement(true, "null");
    this.setNextStatement(true, "null");
    this.setTooltip('Esta função faz com que o robô se vire em x graus para a esquerda ou para a direita.');
  }
};

Blockly.JavaScript['mov_virar'] = function (block) {
  var angle_name = block.getFieldValue('angulo');
  var dropdown_direcao = block.getFieldValue('direcao');
  // TODO: Assemble JavaScript into code variable.
  var code = 'app.robôt.assign_action(virar_angulo,[' + angle_name + ',\'' + dropdown_direcao + '\']);\n';
  return code;
};

Blockly.Blocks['mov_ms'] = {
    init: function () {
        this.setHelpUrl('');
        this.setColour(260);
        this.appendDummyInput()
            .appendField("mover");
        this.appendValueInput("tempo")
            .setCheck("Number")
            .setAlign(Blockly.ALIGN_RIGHT);
        this.appendDummyInput()
            .appendField("ms");
        this.appendDummyInput()
            .setAlign(Blockly.ALIGN_CENTRE)
            .appendField(new Blockly.FieldDropdown([["para frente", "frente"], ["para tras", "tras"]]), "sentido");
        this.setInputsInline(true);
        this.setPreviousStatement(true, "null");
        this.setNextStatement(true, "null");
        this.setTooltip('Esta função faz com que o robô se mova para frente por x ms.');
    }
};

Blockly.JavaScript['mov_ms'] = function (block) {
    var value_tempo = Blockly.JavaScript.valueToCode(block, 'tempo', Blockly.JavaScript.ORDER_ATOMIC);
    var dropdown_sentido = block.getFieldValue('sentido');
    // TODO: Assemble JavaScript into code variable.
     
    var code = 'app.robôt.assign_action(mov_ms,[' + value_tempo + ',\'' + dropdown_sentido + '\']);\n';
    return code;
};

Blockly.Blocks['mov_cm'] = {
    init: function () {
        this.setHelpUrl('');
        this.setColour(260);
        this.appendDummyInput()
            .appendField("mover");
        this.appendValueInput("distancia")
            .setCheck("Number")
            .setAlign(Blockly.ALIGN_RIGHT);
        this.appendDummyInput()
            .appendField("cm");
        this.appendDummyInput()
            .setAlign(Blockly.ALIGN_CENTRE)
            .appendField(new Blockly.FieldDropdown([["para frente", "frente"], ["para tras", "tras"]]), "sentido");
        this.setInputsInline(true);
        this.setPreviousStatement(true, "null");
        this.setNextStatement(true, "null");
        this.setTooltip('Esta função faz com que o robô se mova x cm para frente.');
    }
};


Blockly.JavaScript['mov_cm'] = function (block) {
    var value_distancia = Blockly.JavaScript.valueToCode(block, 'distancia', Blockly.JavaScript.ORDER_ATOMIC);
    var dropdown_sentido = block.getFieldValue('sentido');
    // TODO: Assemble JavaScript into code variable.
    var code = 'app.robôt.assign_action(mov_cm,[' + value_distancia + ',\'' + dropdown_sentido + '\']);\n';
    return code;
};

Blockly.Blocks['parar_ms'] = {
    init: function () {
        this.setHelpUrl('');
        this.setColour(260);
        this.appendDummyInput()
            .appendField("parar");
        this.appendValueInput("tempo")
            .setCheck("Number")
            .setAlign(Blockly.ALIGN_RIGHT);
        this.appendDummyInput()
            .appendField("ms");
        this.setInputsInline(true);
        this.setPreviousStatement(true, "null");
        this.setNextStatement(true, "null");
        this.setTooltip('Esta função faz com que o robô fique parado por x ms.');
    }
};

Blockly.JavaScript['parar_ms'] = function (block) {
    var value_tempo = Blockly.JavaScript.valueToCode(block, 'tempo', Blockly.JavaScript.ORDER_ATOMIC);
    
    // TODO: Assemble JavaScript into code variable.

    var code = 'app.robôt.assign_action(parar_ms,['+ value_tempo +']);\n';
    return code;
};





Blockly.Blocks['mov'] = {
    init: function () {
        this.setHelpUrl('');
        this.setColour(260);
        this.appendDummyInput()
            .appendField("mover");
        this.appendDummyInput()
            .setAlign(Blockly.ALIGN_CENTRE)
            .appendField(new Blockly.FieldDropdown([["para frente", "frente"], ["para tras", "tras"]]), "sentido");
        this.setInputsInline(true);
        this.setPreviousStatement(true, "null");
        this.setNextStatement(true, "null");
        this.setTooltip('Esta função faz com que o robô se mova para a frente até que outro comando de movimento seja dado.');
    }
};

Blockly.JavaScript['mov'] = function (block) {
    var dropdown_sentido = block.getFieldValue('sentido');

    // TODO: Assemble JavaScript into code variable.
    var code = 'app.robôt.assign_action(mov,[\'' + dropdown_sentido + '\']);\n';
    return code;
};

Blockly.Blocks['virar'] = {
    init: function () {
        this.setHelpUrl('');
        this.setColour(260);
        this.appendDummyInput()
            .appendField("virar");
        this.appendDummyInput()
         .setAlign(Blockly.ALIGN_CENTRE)
         .appendField(new Blockly.FieldDropdown([["esquerda", "esquerda"], ["direita", "direita"]]), "direcao");
        this.setInputsInline(true);
        this.setPreviousStatement(true, "null");
        this.setNextStatement(true, "null");
        this.setTooltip('Esta função faz com que o robô se vire para a esquerda ou para a direita  até que outro comando de movimento seja dado.');
    }
};

Blockly.JavaScript['virar'] = function (block) {

    var dropdown_direcao = block.getFieldValue('direcao');

    // TODO: Assemble JavaScript into code variable.
    var code = 'app.robôt.assign_action(virar,[\'' + dropdown_direcao + '\']);\n';
    return code;
};

Blockly.Blocks['parar'] = {
    init: function () {
        this.setHelpUrl('');
        this.setColour(260);
        this.appendDummyInput()
            .appendField("parar");
        this.setInputsInline(true);
        this.setPreviousStatement(true, "null");
        this.setNextStatement(true, "null");
        this.setTooltip('Esta função faz com que o robô pare de se mover até que outro comando de movimento seja dado.');
    }
};

Blockly.JavaScript['parar'] = function (block) {

    //var dropdown_direcao = block.getFieldValue('direcao');

    var code = 'app.robôt.assign_action(parar,[]);\n';
    return code;
};


Blockly.Blocks['ler_acelerometro'] = {
    init: function () {
        this.setHelpUrl('');
        this.setColour(160);
        this.appendDummyInput()
            .appendField("ler acelerometro no eixo");
        this.appendDummyInput()
            .setAlign(Blockly.ALIGN_CENTRE)
            .appendField(new Blockly.FieldDropdown([["x", "x"], ["y", "y"], ["z", "z"]]), "eixo");
        this.setInputsInline(true);
        //this.setPreviousStatement(true, "null");
        //this.setNextStatement(true, "null");
        this.setOutput(true, "Number");
        this.setTooltip('Esta função retorna o valor do acelerometro no eixo especificado.');
    }
};


Blockly.JavaScript['ler_acelerometro'] = function (block) {

    var eixo = block.getFieldValue('eixo');
    var code = 'app.robôt.ler_acelerometro("'+eixo+'")';
    return [code, Blockly.JavaScript.ORDER_NONE];

};


//Blockly.Blocks['verificar_acelerometro'] = {
//    init: function() {
//        //this.setHelpUrl('http://www.example.com/');
//        this.setColour(160);
//        this.appendDummyInput()
//            .appendField("verificar se o acelerometro esta ativo");
//        this.setInputsInline(true);
//        //this.setPreviousStatement(true, "null");
//        //this.setNextStatement(true, "null");
//        this.setOutput(true, "Boolean");
//        this.setTooltip('');
//    }
//};

//Blockly.JavaScript['verificar_acelerometro'] = function (block) {
//    // TODO: Assemble JavaScript into code variable.
   
//    var code = 'app.robôt.verificar_acelerometro()';
//    return [code, Blockly.JavaScript.ORDER_NONE];

//};

//Blockly.Blocks['ativar_acelerometro'] = {
//    init: function () {
//        this.setHelpUrl('');
//        this.setColour(160);
//        this.appendDummyInput()
//            .appendField("ativar acelerometro");
//        this.setInputsInline(true);
//        this.setPreviousStatement(true, "null");
//        this.setNextStatement(true, "null");
//        this.setTooltip('Esta função ativa o acelerometro caso esta esteja desligada');
//    }
//};

//Blockly.JavaScript['ativar_acelerometro'] = function (block) {
//    // TODO: Assemble JavaScript into code variable.
//    var code = 'app.robôt.ativar_acelerometro();\n';
//    return code;
//};

//Blockly.Blocks['verificar_bussola'] = {
//    init: function () {
//        this.setHelpUrl('');
//        this.setColour(160);
//        this.appendDummyInput()
//            .appendField("verificar se a bussola esta ativa");
//        this.setInputsInline(true);
//        this.setOutput(true, "Boolean");
//        this.setTooltip('Esta função verifica se a bussola esta ativa');
//    }
//};

//Blockly.JavaScript['verificar_bussola'] = function (block) {
//    // TODO: Assemble JavaScript into code variable.
//    var code = 'app.robôt.verificar_bussola()';
//    return [code, Blockly.JavaScript.ORDER_NONE];;
//};

Blockly.Blocks['ler_bussola'] = {
    init: function () {
        this.setHelpUrl('');
        this.setColour(160);
        this.appendDummyInput()
            .appendField("ler bussola");
        this.setInputsInline(true);
        this.setOutput(true, "Number");
        this.setTooltip('Esta função retorna a quantos graus o robô se distancia do norte (0 a 360).');
    }
};

Blockly.JavaScript['ler_bussola'] = function (block) {
    // TODO: Assemble JavaScript into code variable.
    var code = 'app.robôt.ler_bussola()';
    return [code, Blockly.JavaScript.ORDER_NONE];
};

//Blockly.Blocks['ativar_bussola'] = {
//    init: function () {
//        this.setHelpUrl('');
//        this.setColour(160);
//        this.appendDummyInput()
//            .appendField("ativar bussola");
//        this.setInputsInline(true);
//        this.setPreviousStatement(true, "null");
//        this.setNextStatement(true, "null");
//        this.setTooltip('Esta função ativa a bussula caso esta esteja desligada');
//    }
//};

//Blockly.JavaScript['ativar_bussola'] = function (block) {
//    // TODO: Assemble JavaScript into code variable.
//    var code = 'app.robôt.ativar_bussola();\n';
//    return code;
//};


Blockly.Blocks['ler_infravermelho'] = {
    init: function () {
        this.setHelpUrl('');
        this.setColour(160);
        this.appendDummyInput()
            .appendField("ler infravermelho");
        this.setInputsInline(true);
        this.setOutput(true, "Number");
        this.setTooltip('Esta função retorna a distância que o robô está de um obstáculo (de 15cm a 70cm).');
    }
};

Blockly.JavaScript['ler_infravermelho'] = function (block) {
    // TODO: Assemble JavaScript into code variable.
    var code = 'app.robôt.ler_infrared()';
    return [code, Blockly.JavaScript.ORDER_NONE];
};

Blockly.Blocks['ler_distancia'] = {
    init: function () {
        this.setHelpUrl('');
        this.setColour(160);
        this.appendDummyInput()
            .appendField("ler distancia");
        this.setInputsInline(true);
        this.setOutput(true, "Number");
        this.setTooltip('Esta função retorna a distância percorrida pelo robô desde que este foi ligado.');
    }
};

Blockly.JavaScript['ler_distancia'] = function (block) {
    // TODO: Assemble JavaScript into code variable.
    var code = 'app.robôt.ler_distancia()';
    return [code, Blockly.JavaScript.ORDER_NONE];
};

Blockly.Blocks['ler_vel_esq'] = {
    init: function () {
        this.setHelpUrl('');
        this.setColour(160);
        this.appendDummyInput()
            .appendField("ler velocidade da roda esquerda");
        this.setInputsInline(true);
        this.setOutput(true, "Number");
        this.setTooltip('Esta função retorna a velocidade da roda esquerda do robô em mm/s.');
    }
};

Blockly.JavaScript['ler_vel_esq'] = function (block) {
    // TODO: Assemble JavaScript into code variable.
    var code = 'app.robôt.ler_speed_left()';
    return [code, Blockly.JavaScript.ORDER_NONE];
};

Blockly.Blocks['ler_vel_dir'] = {
    init: function () {
        this.setHelpUrl('');
        this.setColour(160);
        this.appendDummyInput()
            .appendField("ler velocidade da roda direita");
        this.setInputsInline(true);
        this.setOutput(true, "Number");
        this.setTooltip('Esta função retorna a velocidade da roda direita do robô em mm/s.');
    }
};

Blockly.JavaScript['ler_vel_dir'] = function (block) {
    // TODO: Assemble JavaScript into code variable.
    var code = 'app.robôt.ler_speed_right()';
    return [code, Blockly.JavaScript.ORDER_NONE];
};


Blockly.Blocks['ler_refletancia'] = {
    init: function () {
        this.setHelpUrl('');
        this.setColour(160);
        this.appendDummyInput()
            .appendField("ler refletancia");
        this.setInputsInline(true);
        this.setOutput(true, "List");
        this.setTooltip('Esta função retorna uma lista com 5 elemento representando a leitura de cada um dos 5 sensores (0-branco, 1-preto).');
    }
};

Blockly.JavaScript['ler_refletancia'] = function (block) {
    // TODO: Assemble JavaScript into code variable.
    var code = 'app.robôt.ler_refletancia()';
    return [code, Blockly.JavaScript.ORDER_NONE];
};
